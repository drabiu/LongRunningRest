using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunningTestClient
{
    public class ListeningServer
    {
        public ListeningServer()
        {
            var builder = CreateWebHostBuilder();
            var task = Task.Run(() => {
                builder.Run();
            });

            while (true)
            {
                Thread.Sleep(100);
            }
        }

        public static IWebHost CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning))
                .UseUrls("http://0.0.0.0:5000/")
                .Build();
        }
    }
}
