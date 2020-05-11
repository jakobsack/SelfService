using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SelfService.Lib
{
    public class ThreadData
    {
        public Thread _thread;
        public ManualResetEvent _shutdownEvent;

        public ThreadData(Thread thread, ManualResetEvent shutdownEvent)
        {
            _thread = thread;
            _shutdownEvent = shutdownEvent;
        }
    }
}
