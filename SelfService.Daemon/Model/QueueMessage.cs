using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Model
{
    [DataContract]
    public class QueueMessage
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public long Size { get; set; }
        [DataMember]
        public DateTime SentTime { get; set; }
    }
}
