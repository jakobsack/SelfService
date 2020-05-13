using SelfService.Client.WcfServiceReference;
using SelfService.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
        private string Path = "";
        private Folder Folder = null;
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

            StopTailTask();
            form.listBoxFileSources.Items.Clear();
            form.treeViewFiles.Nodes.Clear();
            form.textBoxFile.Text = "";
            DisableButtons();

            foreach (FileSource fileSource in FileSources)
            {
                form.listBoxFileSources.Items.Add(fileSource.Path);
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
            Path = path;

            StopTailTask();
            form.treeViewFiles.Nodes.Clear();
            form.textBoxFile.Text = "";
            DisableButtons();

            using (WcfClient client = Helper.GetWcfClient(Hostname))
            {
                var result = client.GetFolder(path);
                form.SetStatusLabel(result.Success, result.Error);
                if (result.Success)
                {
                    Folder = result.Data;
                }
                else
                {
                    Folder = null;
                }
            }

            if (Folder == null)
            {
                return;
            }

            TreeNode rootNode = GetFolderyNode(Folder);
            foreach (TreeNode node in rootNode.Nodes)
            {
                form.treeViewFiles.Nodes.Add(node);
            }
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
                form.textBoxFile.Text = reader.ReadToEnd();
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
            string path = Path + @"\" + fullPath;

            form.textBoxFile.Text = "";
            if (FileList.Contains(path))
            {
                FilePath = path;
                form.buttonDownload.Enabled = true;
                form.buttonShow.Enabled = true;
                form.buttonTail.Enabled = true;
            }
            else
            {
                DisableButtons();
            }
        }

        private TreeNode GetFolderyNode(Folder folder)
        {
            TreeNode treeNode = new TreeNode
            {
                Text = System.IO.Path.GetFileName(folder.Name),
            };

            foreach (Folder child in folder.Folders)
            {
                treeNode.Nodes.Add(GetFolderyNode(child));
            }

            foreach (string fileName in folder.Files)
            {
                treeNode.Nodes.Add(System.IO.Path.GetFileName(fileName));
                FileList.Add(fileName);
            }
            return treeNode;
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
                            form.textBoxFile.Invoke((MethodInvoker)delegate {
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
                                form.textBoxFile.Invoke((MethodInvoker)delegate {
                                    // Running on the UI thread
                                    form.textBoxFile.Text = "";
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
                        form.textBoxFile.Invoke((MethodInvoker)delegate {
                            // Running on the UI thread
                            form.textBoxFile.Text += text;
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
