using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Model
{
    [DataContract]
    public class Folder
    {
        [DataMember]
        public string Name{ get; set; }

        [DataMember]
        public List<Folder> Folders { get; set; } = new List<Folder>();

        [DataMember]
        public List<string> Files { get; set; } = new List<string>();
    }
}
