using Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateDefaultBuilder(args).ConfigureLogging((hostingContext, config) => { config.ClearProviders(); })
                         .Build().RunAsync().ConfigureAwait(false);
        }

        public static IWebHostBuilder CreateDefaultBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                 .UseLogging();
    }
}
