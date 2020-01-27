using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace LongRunningTestClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                Console.WriteLine("Job done");

                return Task.CompletedTask;
            });
        }
    }
}
