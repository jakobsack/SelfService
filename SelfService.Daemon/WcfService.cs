using Microsoft.Win32;
using SelfService.Daemon.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Messaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon
{
    public class WcfService : IWcf
    {
        public Result<List<QueueMessage>> GetQueueMessageList(string queueName)
        {
            Result<List<QueueMessage>> result = new Result<List<QueueMessage>>();

            try
            {
                List<QueueMessage> queueMessages = new List<QueueMessage>();
                using (MessageQueue queue = new MessageQueue(queueName))
                {
                    queue.Formatter = new BinaryMessageFormatter();
                    queue.MessageReadPropertyFilter.ArrivedTime = true;

                    using (MessageEnumerator enumerator = queue.GetMessageEnumerator2())
                    {
                        while (enumerator.MoveNext())
                        {
                            Message peekMessage = queue.PeekById(enumerator.Current.Id);
                            QueueMessage queueMessage = new QueueMessage
                            {
                                Id = peekMessage.Id,
                                Label = peekMessage.Label,
                                Body = "",
                                SentTime = peekMessage.ArrivedTime,
                                Size = peekMessage.BodyStream.Length,
                            };

                            queueMessages.Add(queueMessage);
                        }
                    }
                }

                result.Data = queueMessages;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        public Result<List<Queue>> GetQueues()
        {
            Result<List<Queue>> result = new Result<List<Queue>>();

            try
            {
                List<Queue> queues = new List<Queue>();
                MessageQueue[] QueueList = MessageQueue.GetPrivateQueuesByMachine(".");
                foreach (MessageQueue queueItem in QueueList)
                {
                    int messages = 0;
                    using (MessageEnumerator enumerator = queueItem.GetMessageEnumerator2())
                    {
                        while (enumerator.MoveNext())
                        {
                            messages++;
                        }
                    }

                    Queue queue = new Queue
                    {
                        Path = queueItem.Path,
                        Label = queueItem.Label,
                        Messages = messages,
                    };
                    queues.Add(queue);
                }

                result.Data = queues;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        public Result<QueueMessage> GetQueueMessage(string queueName, string messageId)
        {
            Result<QueueMessage> result = new Result<QueueMessage>();

            try
            {
                QueueMessage queueMessage = null;

                using (MessageQueue queue = new MessageQueue(queueName))
                {
                    queue.Formatter = new BinaryMessageFormatter();
                    queue.MessageReadPropertyFilter.ArrivedTime = true;

                    Message peekMessage = queue.PeekById(messageId);

                    long size = peekMessage.BodyStream.Length;
                    byte[] body = new byte[size];
                    using (BinaryReader reader = new BinaryReader(peekMessage.BodyStream))
                    {
                        body = reader.ReadBytes((int)size);
                    }

                    queueMessage = new QueueMessage
                    {
                        Id = peekMessage.Id,
                        Label = peekMessage.Label,
                        Body = Convert.ToBase64String(body),
                        SentTime = peekMessage.ArrivedTime,
                        Size = size,
                    };
                    queueMessage.Size = queueMessage.Body.Length;
                }

                result.Data = queueMessage;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        public Result<List<RegistryItem>> GetRegistry()
        {
            Result<List<RegistryItem>> result = new Result<List<RegistryItem>>();
            try
            {
                List<RegistryItem> registryItems = new List<RegistryItem>();
                string[] paths = File.ReadAllLines("registry.txt");
                foreach (string path in paths.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    registryItems.Add(GetRegistry(path));
                }

                result.Data = registryItems;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        private RegistryItem GetRegistry(string path)
        {
            RegistryItem registryItem = new RegistryItem
            {
                KeyName = path
            };

            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(path, false))
            {
                foreach (string valueName in registryKey.GetValueNames())
                {
                    RegistryValueKind kind = registryKey.GetValueKind(valueName);
                    object value = registryKey.GetValue(valueName);
                    string stringValue = "";
                    switch (kind)
                    {
                        case RegistryValueKind.MultiString:
                            string[] values = (string[])value;
                            stringValue = string.Join("\n", values);
                            break;

                        case RegistryValueKind.Binary:
                            byte[] bytes = (byte[])value;
                            for (int i = 0; i < bytes.Length; i++)
                            {
                                // Display each byte as two hexadecimal digits.
                                stringValue += string.Format("{0:X2}", bytes[i]);
                            }
                            break;

                        default:
                            stringValue = value.ToString();
                            break;
                    }

                    RegistryValue registryValue = new RegistryValue
                    {
                        Name = valueName,
                        Value = stringValue,
                        Type = kind.ToString()
                    };

                    registryItem.RegistryValues.Add(registryValue);
                }

                string[] subKeys = registryKey.GetSubKeyNames();
                foreach (string keyName in registryKey.GetSubKeyNames())
                {
                    string keyPath = path + @"\" + keyName;
                    registryItem.RegistryItems.Add(GetRegistry(keyPath));
                }
            }

            return registryItem;
        }

        public Result<List<FileSource>> GetFileSources()
        {
            Result<List<FileSource>> result = new Result<List<FileSource>>();
            try
            {
                List<FileSource> fileSources = new List<FileSource>();
                string[] paths = ListFileSources();
                foreach (string path in paths)
                {
                    fileSources.Add(new FileSource
                    {
                        Path = path
                    });
                }

                result.Data = fileSources;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        private string[] ListFileSources()
        {
            List<string> availableFiles = new List<string>();
            string[] paths = File.ReadAllLines("files.txt");
            foreach (string path in paths.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                if (path.ToLower().StartsWith("%appdata%"))
                {
                    foreach (string user in Directory.GetDirectories(@"C:\Users"))
                    {
                        string userPath = user + @"\appdata\roaming" + path.Substring(9);

                        if (Directory.Exists(userPath))
                        {
                            availableFiles.Add(userPath);
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(path))
                    {
                        availableFiles.Add(path);
                    }
                }
            }

            return availableFiles.ToArray();
        }

        public Result<Folder> GetFolder(string path)
        {
            Result<Folder> result = new Result<Folder>();
            try
            {
                string[] paths = ListFileSources();
                if (!paths.Contains(path))
                {
                    throw new Exception("Requested folder is not part of the allowed ones.");
                }

                Folder folder = GetFolderContent(path);

                result.Data = folder;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        private Folder GetFolderContent(string path)
        {
            Folder folder = new Folder
            {
                Name = path,
            };

            foreach (string fileName in Directory.GetFiles(path))
            {
                folder.Files.Add(fileName);
            }

            foreach (string folderName in Directory.GetDirectories(path))
            {
                try
                {
                    folder.Folders.Add(GetFolderContent(folderName));
                }
                catch
                {
                    // NOOP. Let's hope the other folders go well.
                }

            }

            return folder;
        }

        public Result<Download> DownloadFile(string path, long offset)
        {
            Result<Download> result = new Result<Download>();
            try
            {
                path = Path.GetFullPath(path);
                string[] paths = ListFileSources();
                if (!paths.Any(x => path.StartsWith(x)))
                {
                    throw new Exception("Requested folder is not part of the allowed ones.");
                }

                byte[] byteArray;

                using (MemoryStream rawStream = new MemoryStream(1024))
                using (MemoryStream zipStream = new MemoryStream(1024))
                {
                    FileInfo fileToCompress = new FileInfo(path);
                    using (FileStream fileStream = fileToCompress.OpenRead())
                    {
                        if (fileStream.Length <= offset)
                        {
                            throw new Exception("Fils is smaller than offset.");
                        }

                        fileStream.Position = offset;

                        try
                        {
                            fileStream.CopyTo(rawStream);
                        }
                        catch
                        {
                            // NOOP. Take what we got so far
                        }
                    }

                    rawStream.Position = 0;
                    using (GZipStream compressionStream = new GZipStream(zipStream, CompressionMode.Compress, leaveOpen: true))
                    {
                        rawStream.CopyTo(compressionStream);
                    }

                    zipStream.Position = 0;
                    int bytesToRead = zipStream.Length > int.MaxValue ? int.MaxValue : (int)zipStream.Length;
                    byteArray = new byte[bytesToRead];
                    zipStream.Read(byteArray, 0, bytesToRead);
                }

                Download download = new Download
                {
                    Data = Convert.ToBase64String(byteArray),
                };

                result.Data = download;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }
    }
}
