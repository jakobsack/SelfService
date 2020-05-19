using SelfService.Daemon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon
{
    [ServiceContract()]
    public interface IWcf
    {
        [OperationContract]
        Result<List<RegistryItem>> GetRegistry();

        [OperationContract]
        Result<List<Queue>> GetQueues();

        [OperationContract]
        Result<List<QueueMessage>> GetQueueMessageList(string queueName);

        [OperationContract]
        Result<QueueMessage> GetQueueMessage(string queueName, string messageId);

        [OperationContract]
        Result<List<FileSource>> GetFileSources();

        [OperationContract]
        Result<Folder> GetFolder(string path);

        [OperationContract]
        Result<Download> DownloadFile(string path, long offset);

        [OperationContract]
        Result<List<ServiceItem>> GetServices();
    }
}
