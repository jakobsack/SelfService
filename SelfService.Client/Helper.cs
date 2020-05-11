using SelfService.Client.WcfServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Client
{
    public static class Helper
    {
        public static WcfClient GetWcfClient(string hostname)
        {
            return new WcfClient("BasicHttpBinding_IWcf", "http://" + hostname + ":52465/SelfService/Wcf");
        }
    }
}
