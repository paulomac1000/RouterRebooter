using RouterRebooter.Core;
using RouterRebooter.Helpers;
using System.ServiceProcess;
using System.Threading;

namespace RouterRebooter
{
    internal static class Program
    {
        private static void Main()
        {
            if (!AppSettings.IsDebug)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new ServiceCore()
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                var scheduler = new Scheduler();
                scheduler.Start();
                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}