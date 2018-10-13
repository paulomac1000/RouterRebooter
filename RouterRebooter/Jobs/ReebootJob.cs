using Quartz;
using RouterRebooter.Helpers;
using System;
using System.Threading.Tasks;

namespace RouterRebooter.Jobs
{
    public class ReebootJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            if (ConnectionHelper.CheckSshCanConnect())
                return Task.CompletedTask;

            if (!ConnectionHelper.CheckTelnetCanConnect())
                throw new Exception("Even telnet not working. Check settings.");

            ConnectionHelper.TelnetSendReboot();
            return Task.CompletedTask;
        }
    }
}