using SelfService.Daemon.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Provider
{
    internal static class FileProvider
    {
        internal static Result<List<FileSource>> GetFileSources()
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

        private static string[] ListFileSources()
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

        internal static Result<Folder> GetFolder(string path)
        {
            Result<Folder> result = new Result<Folder>();
            try
            {
                string[] paths = ListFileSources();
                if (!paths.Any(x => path.StartsWith(x)))
                {
                    throw new Exception("Requested folder is not part of the allowed ones.");
                }

                Folder folder = new Folder
                {
                    Name = path,
                };

                foreach (string fileName in Directory.GetFiles(path))
                {
                    folder.Files.Add(Path.GetFileName(fileName));
                }

                foreach (string folderName in Directory.GetDirectories(path))
                {
                    folder.Folders.Add(Path.GetFileName(folderName));
                }

                result.Data = folder;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        internal static Result<Download> DownloadFile(string path, long offset)
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
                    using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete, 65536, FileOptions.SequentialScan))
                    {
                        if (fileStream.Length < offset)
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
