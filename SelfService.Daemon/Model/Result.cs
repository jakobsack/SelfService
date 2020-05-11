using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Model
{
    [DataContract]
    public class Result<T>
    {
        [DataMember]
        public bool Success { get; set; } = true;
        [DataMember]
        public T Data { get; set; }
        [DataMember]
        public string Error { get; set; } = null;
    }
}
