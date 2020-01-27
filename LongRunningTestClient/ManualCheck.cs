using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunningTestClient
{
    public class ManualCheck
    {
        public async Task CheckJobDone()
        {
            var output = await new HttpClient().GetAsync("https://localhost:44347/idletask/run");
            var location = output.Headers.Location;
            Console.WriteLine("Started task");
            var checkJobUrl = $"https://localhost:44347/idletask/{location}";
            var jobStatus = await new HttpClient().GetStringAsync(checkJobUrl);
            while (jobStatus != "Done")
            {
                Thread.Sleep(1000);
                jobStatus = await new HttpClient().GetStringAsync(checkJobUrl);
                Console.WriteLine("Checking...");
            }

            if (jobStatus == "Done")
            {
                Console.WriteLine("Job Done");
            }
        }
    }
}
