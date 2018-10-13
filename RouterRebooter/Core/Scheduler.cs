using Quartz;
using Quartz.Impl;
using RouterRebooter.Helpers;
using RouterRebooter.Jobs;
using System.Collections.Specialized;

namespace RouterRebooter.Core
{
    public class Scheduler
    {
        private const string JobName = "routerReebootJob";
        private const string TriggerName = "routerReebootTrigger";

        private readonly IScheduler scheduler;

        public Scheduler()
        {
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            ISchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = factory.GetScheduler().GetAwaiter().GetResult();
            scheduler.Start().GetAwaiter().GetResult();
        }

        public void Start()
        {
            IJobDetail job = JobBuilder.Create<ReebootJob>()
                .WithIdentity(new JobKey(JobName))
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(new TriggerKey(TriggerName))
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(AppSettings.CheckingIntervalInMinutes)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();
        }

        public void Stop()
        {
            scheduler.DeleteJob(new JobKey(JobName));
        }
    }
}