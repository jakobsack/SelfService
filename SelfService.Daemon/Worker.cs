using SelfService.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SelfService.Daemon
{
    class Worker
    {
        private  static ServiceHost serviceHost = null;

        public static void Run(ManualResetEvent shutdownEvent)
        {
            ResetWcfService();

            while (!shutdownEvent.WaitOne(0))
            {
                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception exception)
                {
                    Logger.Error("Problem with runner. Restarting whole environment.");
                    Logger.Error(exception.ToString());
                    ResetWcfService();
                    continue;
                }
            }
        }

        private static void ResetWcfService()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
            serviceHost = new ServiceHost(typeof(WcfService));
            serviceHost.Open();
        }
    }
}
