using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Scheduler
{
    public class RecurringJobSchduler<T> where T : class
    {
        public interface IJob<T> //Job interface
        {
            Task Execute<U>();
        }

        public abstract class Job<T> : IJob<T> // base job class
        {
            public async Task Execute<U>()
            {
                object item = new object();
                BackgroundJob.Enqueue<IWorkerService<U>>(x => x.WorkerTask(item));
            }
        }

        public class MyJob : Job<MyJob> // a job
        {
            //some other stuff
        }

        public class MyWorkerService : IWorkerService<MyWorkerService>
        {
            public async Task WorkerTask(object id)
            {
                //some implementation
            }
        }

        public interface IWorkerService<T>
        {
            Task WorkerTask(object item);
        }

        public void AddJob()
        {
            RecurringJob.AddOrUpdate<IJob<T>>("myJob", c => c.Execute<MyWorkerService>(), Cron.Minutely(), TimeZoneInfo.Local);
        }
    }
}
