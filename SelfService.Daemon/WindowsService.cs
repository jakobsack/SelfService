using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SelfService.Lib;

namespace SelfService.Daemon
{
    public partial class WindowsService : ServiceBase
    {
        private List<ThreadData> _threads = new List<ThreadData>();

        public WindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Flow("Starting worker thread");

            ManualResetEvent shutDownEvent = new ManualResetEvent(false);
            Thread t = new Thread(() => Worker.Run(shutDownEvent))
            {
                Name = "RunPowershellServiceThread",
                IsBackground = true
            };
            t.Start();
            _threads.Add(new ThreadData(t, shutDownEvent));

            Logger.Flow("Worker thread started");
        }

        protected override void OnStop()
        {
            Logger.Flow("Stopping worker thread");

            Logger.Flow("Shutting down.");
            foreach (ThreadData threadData in _threads)
            {
                threadData._shutdownEvent.Set();
                if (!threadData._thread.Join(10000))
                {
                    // give the thread 10 seconds to stop
                    threadData._thread.Abort();
                }
            }

            Logger.Flow("Worker thread stopped");
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
