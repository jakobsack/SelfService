using Microsoft.Win32;
using SelfService.Daemon.Model;
using SelfService.Daemon.Provider;
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
            return MsmqProvider.GetQueueMessageList(queueName);
        }

        public Result<List<Queue>> GetQueues()
        {
            return MsmqProvider.GetQueues();
        }

        public Result<QueueMessage> GetQueueMessage(string queueName, string messageId)
        {
            return MsmqProvider.GetQueueMessage(queueName, messageId);
        }

        public Result<List<RegistryItem>> GetRegistry()
        {
            return RegistryProvider.GetRegistry();
        }

        public Result<List<FileSource>> GetFileSources()
        {
            return FileProvider.GetFileSources();
        }

        public Result<Folder> GetFolder(string path)
        {
            return FileProvider.GetFolder(path);
        }

        public Result<Download> DownloadFile(string path, long offset)
        {
            return FileProvider.DownloadFile(path, offset);
        }

        public Result<List<ServiceItem>> GetServices()
        {
            return ServiceProvider.GetServices();
        }
    }
}
