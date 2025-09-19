using System;
using System.ServiceProcess;

namespace SimpleService
{
    static class Program
    {
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                var svc = new SimpleService();
                svc.TestRun(); // console mode for quick test
            }
            else
            {
                ServiceBase.Run(new ServiceBase[] { new SimpleService() });
            }
        }
    }
}
