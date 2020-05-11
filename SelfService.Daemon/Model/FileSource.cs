using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Model
{
    [DataContract]
    public class FileSource
    {
        [DataMember]
        public string Path { get; set; }
    }
}
