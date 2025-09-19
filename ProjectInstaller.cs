using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace SimpleService
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;

            var serviceInstaller = new ServiceInstaller();
            serviceInstaller.ServiceName = SimpleService.SvcName;
            serviceInstaller.DisplayName = "Simple Service Demo";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
