using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Model
{
    [DataContract]
    public class Queue
    {
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public string Label { get; set; }
        [DataMember]
        public int Messages { get; set; }
    }
}
