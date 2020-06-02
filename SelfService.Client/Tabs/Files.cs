using SelfService.Client.WcfServiceReference;
using SelfService.Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SelfService.Client.Tabs
{
    class Files
    {
        private string Hostname = "";
        private MainForm form;
        private FileSource[] FileSources = null;
        private string CurrentPath = "";
        private Folder Folder = null;
        private Dictionary<string, List<string>> Siblings = new Dictionary<string, List<string>>();
        private List<string> FileList = new List<string>();
        private string FilePath = null;
        long Position = 0;

        private List<ThreadData> _threads = new List<ThreadData>();

        public Files(MainForm mainForm)
        {
            form = mainForm;
        }

        internal void GetFileSources(string hostname)
        {
            Hostname = hostname;
            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.GetFileSources();
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    FileSources = result.Data;
                }
                else
                {
                    FileSources = new FileSource[0];
                }
            }

            FileList.Clear();
            CurrentPath = "";
            Siblings[CurrentPath] = FileSources.Select(x => x.Path).ToList();
            RenderFileList();
        }

        private void RenderFileList()
        {
            // First do some cleanup
            Position = 0;
            StopTailTask();
            DisableButtons();
            form.listViewFileSources.Items.Clear();
            form.listBoxFile.Items.Clear();

            if (CurrentPath == String.Empty)
            {
                // No Path. We list the file sources
                foreach (FileSource fileSource in FileSources)
                {
                    ListViewItem listViewItem = new ListViewItem(fileSource.Path, 0);
                    form.listViewFileSources.Items.Add(listViewItem);
                }
            }
            else if (Folder != null)
            {
                // We have a path and a folder. Render!
                form.listViewFileSources.Items.Add(new ListViewItem("..", 1));
                form.listViewFileSources.Items.Add(new ListViewItem(".", 2));

                foreach (string folder in Folder.Folders)
                {
                    ListViewItem listViewItem = new ListViewItem(folder, 3);
                    form.listViewFileSources.Items.Add(listViewItem);
                }

                foreach (string file in Folder.Files)
                {
                    ListViewItem listViewItem = new ListViewItem(file, 4);
                    form.listViewFileSources.Items.Add(listViewItem);
                }
            }

            ResetLocationBar();
        }

        internal void SelectItem(ListViewItem listViewItem)
        {
            // Get the new path the user requests
            string newPath = CurrentPath == "" ? listViewItem.Text : System.IO.Path.Combine(CurrentPath, listViewItem.Text);

            if(listViewItem.Text == ".")
            {
                // We simply update the folder
                GetFolder(CurrentPath);
            }
            else if(listViewItem.Text == "..")
            {
                // We want to go one folder up
                string newFolder = System.IO.Path.GetDirectoryName(CurrentPath);

                if (!FileSources.Any(x => newFolder.StartsWith(x.Path)))
                {
                    // We are at top level again. Refetch 
                    GetFileSources(Hostname);
                }
                else
                {
                    // Get parent directory
                    GetFolder(newFolder);
                }
            }
            else if (FileList.Contains(newPath))
            {
                SelectFile(newPath);
            }
            else
            {
                GetFolder(newPath);
            }
        }

        private void ResetLocationBar()
        {
            form.menuStripLocation.Items.Clear();
            form.menuStripLocation.Items.Add("You are here:");

            if(CurrentPath == "")
            {
                return;
            }

            string menuPath = "";
            while (true)
            {
                if (!Siblings.ContainsKey(menuPath))
                {
                    return;
                }

                string part = Siblings[menuPath].FirstOrDefault(x => CurrentPath == Path.Combine(menuPath, x) || CurrentPath.StartsWith(Path.Combine(menuPath, x) + Path.DirectorySeparatorChar)); 
                if (string.IsNullOrEmpty(part)){
                    return;
                }

                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = part;
                item.Tag = Path.Combine(menuPath, part);

                foreach (string alternative in Siblings[menuPath])
                {
                    ToolStripMenuItem subitem = new ToolStripMenuItem();
                    subitem.Text = alternative;
                    subitem.Tag = Path.Combine(menuPath, alternative);
                    subitem.Click += new System.EventHandler(form.JumpToFolder);
                    if(alternative == part)
                    {
                        subitem.Font = new Font(subitem.Font, FontStyle.Bold);
                    }

                    item.DropDownItems.Add(subitem);
                }

                form.menuStripLocation.Items.Add(item);

                // Last step: itrerate
                menuPath = Path.Combine(menuPath, part);
            }
        }

        private void DisableButtons()
        {
            form.buttonDownload.Enabled = false;
            form.buttonShow.Enabled = false;
            form.buttonTail.Enabled = false;
        }

        internal void GetFolder(string path)
        {
            CurrentPath = path;

            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.GetFolder(path);
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    Folder = result.Data;

                    FileList = Folder.Files.Select(x => Path.Combine(CurrentPath, x)).ToList();
                    Siblings[CurrentPath] = Folder.Folders.ToList();
                }
                else
                {
                    Folder = null;
                }
            }

            RenderFileList();
        }

        internal void StartTailTask()
        {
            ManualResetEvent shutDownEvent = new ManualResetEvent(false);
            Thread t = new Thread(() => TailWorker(Hostname, FilePath, Position, form, shutDownEvent))
            {
                Name = "TailWorker",
                IsBackground = true
            };
            t.Start();
            _threads.Add(new ThreadData(t, shutDownEvent));
        }

        internal void StopTailTask()
        {
            foreach (ThreadData threadData in _threads)
            {
                threadData._shutdownEvent.Set();
                if (!threadData._thread.Join(10000))
                {
                    // give the thread 10 seconds to stop
                    threadData._thread.Abort();
                }
            }

            _threads.Clear();
            form.buttonTail.Text = "Tail";
        }

        internal void DownloadFile()
        {
            StopTailTask();
            form.saveFileDialog1.FileName = System.IO.Path.GetFileName(FilePath);
            DialogResult dialogResult = form.saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                byte[] bytes = GetFile(0);
                File.WriteAllBytes(form.saveFileDialog1.FileName, bytes);
            }
        }

        internal void ShowFile()
        {
            StopTailTask();
            byte[] bytes = GetFile(0);
            Position = bytes.Length;

            using (MemoryStream zipStream = new MemoryStream(bytes))
            using (StreamReader reader = new StreamReader(zipStream, Encoding.UTF8, true))
            {
                reader.Peek();
                form.AddTextToListBoxFile(reader.ReadToEnd());
            }
        }

        internal void TailFile()
        {
            if (form.buttonTail.Text == "Tail")
            {
                StartTailTask();
                form.buttonTail.Text = "Stop Tail";
            }
            else
            {
                StopTailTask();
            }
        }

        private byte[] GetFile(int position)
        {
            byte[] bytes = new byte[0];
            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.DownloadFile(FilePath, position);
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    byte[] rawBytes = Convert.FromBase64String(result.Data.Data);
                    bytes = UncompressData(rawBytes);
                }
            }

            return bytes;
        }

        internal void SelectFile(string fullPath)
        {
            form.listBoxFile.Items.Clear();

            StopTailTask();
            FilePath = fullPath;
            Position = 0;
            form.buttonDownload.Enabled = true;
            form.buttonShow.Enabled = true;
            form.buttonTail.Enabled = true;
        }

        private static byte[] UncompressData(byte[] rawBytes)
        {
            using (MemoryStream zipStream = new MemoryStream(rawBytes))
            using (MemoryStream rawStream = new MemoryStream(100))
            {
                using (GZipStream decompressionStream = new GZipStream(zipStream, CompressionMode.Decompress, leaveOpen: true))
                {
                    decompressionStream.CopyTo(rawStream);
                }

                rawStream.Position = 0;
                int bytesToRead = rawStream.Length > int.MaxValue ? int.MaxValue : (int)rawStream.Length;
                byte[] bytes = new byte[bytesToRead];
                rawStream.Read(bytes, 0, bytesToRead);

                return bytes;
            }
        }

        private static void TailWorker(string hostname, string filePath, long firstPosition, MainForm form, ManualResetEvent shutdownEvent)
        {
            long position = firstPosition;
            DateTime lastTimeStamp = DateTime.MinValue;
            TimeSpan timeOut = new TimeSpan(0, 0, 3);

            while (!shutdownEvent.WaitOne(0))
            {
                try
                {
                    if (DateTime.Now - lastTimeStamp > timeOut)
                    {
                        lastTimeStamp = DateTime.Now;

                        byte[] bytes = new byte[0];
                        using (WcfClient client = Helper.GetWcfClient(hostname))
                        {
                            var result = client.DownloadFile(filePath, position);
                            form.Invoke((MethodInvoker)delegate
                            {
                                // Running on the UI thread
                                form.SetStatusLabel(result.Success, result.Error);
                            });
                            if (result.Success)
                            {
                                byte[] rawBytes = Convert.FromBase64String(result.Data.Data);
                                bytes = UncompressData(rawBytes);
                                position += bytes.Length;
                            }
                            else
                            {
                                lastTimeStamp = DateTime.MinValue;
                                position = 0;
                                form.Invoke((MethodInvoker)delegate
                                {
                                    // Running on the UI thread
                                    form.listBoxFile.Items.Clear();
                                    form.SetStatusLabel(result.Success, result.Error);
                                });
                            }
                        }

                        string text = string.Empty;

                        using (MemoryStream zipStream = new MemoryStream(bytes))
                        using (StreamReader reader = new StreamReader(zipStream, Encoding.UTF8, true))
                        {
                            reader.Peek();
                            text = reader.ReadToEnd();
                        }

                        // Carefully update
                        form.Invoke((MethodInvoker)delegate
                        {
                            // Running on the UI thread
                            form.AddTextToListBoxFile(text);
                        });
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
