using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace RouterRebooter.Core
{
    public partial class ServiceCore : ServiceBase
    {
        private readonly Scheduler scheduler;

        public ServiceCore()
        {
            InitializeComponent();
            scheduler = new Scheduler();
        }

        protected override void OnStart(string[] args)
        {
            Task.Run(() => OnStartContinue());
        }

        protected override void OnStop()
        {
            scheduler.Stop();
        }

        private void OnStartContinue()
        {
            scheduler.Start();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}