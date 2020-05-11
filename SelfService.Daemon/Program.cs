using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon
{
    static class Program
    {
        static int Main(string[] args)
        {
			// install service
			if (args.Length > 0 && (args[0].ToLower() == "/install"))
			{
				System.Configuration.Install.TransactedInstaller ti = new System.Configuration.Install.TransactedInstaller();
				ti.Installers.Add(new WindowsServiceInstaller());
				ti.Context = new System.Configuration.Install.InstallContext("", null);
				ti.Context.Parameters["assemblypath"] = System.Reflection.Assembly.GetExecutingAssembly().Location;
				ti.Install(new System.Collections.Hashtable());
				return 0;
			}

			// uninstall service
			if (args.Length > 0 && (args[0].ToLower() == "/uninstall"))
			{
				System.Configuration.Install.TransactedInstaller ti = new System.Configuration.Install.TransactedInstaller();
				ti.Installers.Add(new WindowsServiceInstaller());
				ti.Context = new System.Configuration.Install.InstallContext("", null);
				ti.Context.Parameters["assemblypath"] = System.Reflection.Assembly.GetExecutingAssembly().Location;
				ti.Uninstall(null);
				return 0;
			}

			// Always run in the directory wherre the exe file is located
			Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			// run in a shell or as a service
			if (Environment.UserInteractive)
			{
				WindowsService service = new WindowsService();
				service.TestStartupAndStop(args);
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[]
				{
					new WindowsService()
				};
				ServiceBase.Run(ServicesToRun);
			}

			return 0;
		}
	}
}
