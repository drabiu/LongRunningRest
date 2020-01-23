using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunningRest
{
    public class DoSomething : IDoSomething
    {
        private Random _random;
        private IDictionary<Guid, string> _tasks;

        public DoSomething()
        {
            _random = new Random();
            _tasks = new Dictionary<Guid, string>();
        }

        public Guid Idle()
        {
            var id = Guid.NewGuid();
            var state = id.ToString();
            _tasks.Add(id, "Queued");
            ThreadPool.QueueUserWorkItem(DoSomeWork, state);

            return id;
        }

        public string CheckStatus(Guid taskId)
        {
            return _tasks[taskId];
        }

        public async Task Notify(string url)
        {
            var output = await new HttpClient().GetAsync("https://localhost:44347/idletask/run");
            var location = output.Headers.Location;
            var checkJobUrl = $"https://localhost:44347/idletask/{location}";
            var jobStatus = await new HttpClient().GetStringAsync(checkJobUrl);
            while (jobStatus != "Done")
            {
                Thread.Sleep(1000);
                jobStatus = await new HttpClient().GetStringAsync(checkJobUrl);
            }

            if (jobStatus == "Done")
            {
                await new HttpClient().PostAsync(url, new StringContent("job done"));
            }
        }

        private void DoSomeWork(object? state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            var guid = new Guid((string)state);
            _tasks[guid] = "Started";

            Thread.Sleep(_random.Next(10000, 30000));
            _tasks[guid] = "Done";
        }
    }
}
