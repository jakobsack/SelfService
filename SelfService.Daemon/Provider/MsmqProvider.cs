using SelfService.Daemon.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Provider
{
    internal static class MsmqProvider
    {
        internal static Result<List<QueueMessage>> GetQueueMessageList(string queueName)
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

        internal static Result<List<Queue>> GetQueues()
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

        internal static Result<QueueMessage> GetQueueMessage(string queueName, string messageId)
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
    }
}
