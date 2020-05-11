using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Model
{
    [DataContract]
    public class RegistryItem
    {
        [DataMember]
        public string KeyName { get; set; }
        [DataMember]
        public List<RegistryItem> RegistryItems { get; set; } = new List<RegistryItem>();
        [DataMember]
        public List<RegistryValue> RegistryValues { get; set; } = new List<RegistryValue>();
    }
}
