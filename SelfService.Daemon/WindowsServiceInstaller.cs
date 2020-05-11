using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace SelfService.Daemon
{
    [RunInstaller(true)]
    public partial class WindowsServiceInstaller : System.Configuration.Install.Installer
    {
        public WindowsServiceInstaller()
        {
            InitializeComponent();
        }

		protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
		{
			// Apply the ServiceDependencies specified in the app.config file
			// http://raquila.com/software/configure-app-config-application-settings-during-msi-install/
			string assemblyPath = Context.Parameters["assemblypath"];
			Configuration config = ConfigurationManager.OpenExeConfiguration(assemblyPath);
			KeyValueConfigurationElement dependencyElement = config.AppSettings.Settings["ServiceDependencies"];

			string dependencies = dependencyElement != null ? dependencyElement.Value : null;
			if (dependencies != null)
			{
				List<string> combinedDependencies = new List<string>(serviceInstaller.ServicesDependedOn);
				combinedDependencies.AddRange(dependencies.Split(','));

				serviceInstaller.ServicesDependedOn = combinedDependencies.Distinct().ToArray();
			}

			base.OnBeforeInstall(savedState);
		}

		private void serviceInstaller_AfterInstall(object sender, InstallEventArgs e)
		{

		}

		private void serviceProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
		{

		}
	}
}
