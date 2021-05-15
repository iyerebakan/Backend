using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public static class LoggingHostingExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder)
            => webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var options = context.Configuration.GetSection("Logging").Get<LoggingOptions>();

                if (!Enum.TryParse<LogEventLevel>(options.LogLevel.Default, true, out var level))
                {
                    level = LogEventLevel.Information;
                }

                if (!Enum.TryParse<LogEventLevel>(options.LogLevel.Microsoft, true, out var mlevel))
                {
                    mlevel = LogEventLevel.Error;
                }

                if (!Enum.TryParse<LogEventLevel>(options.LogLevel.System, true, out var slevel))
                {
                    slevel = LogEventLevel.Error;
                }

                loggerConfiguration.Enrich.FromLogContext()
                    .MinimumLevel.Is(level)
                    .MinimumLevel.Override("Microsoft", mlevel)
                    .MinimumLevel.Override("System", slevel);

                if (options.ConsoleEnabled)
                {
                    var ipAddress = string.Empty;
                    foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress += (string.IsNullOrEmpty(ipAddress) ? string.Empty : "|") + ip;
                        }
                    }

                    loggerConfiguration
                        .Enrich.WithProperty("IpAddress", ipAddress)
                        .Enrich.WithProperty("LogType", LogTypeEnum.Default)
                        .Enrich.WithMachineName()
                        .Enrich.WithExceptionDetails()
                        .WriteTo.Console(new RenderedCompactJsonFormatter());
                }

                if (options.FileEnabled)
                {
                    loggerConfiguration.WriteTo.File(new CompactJsonFormatter(), "logs/logs", rollingInterval: RollingInterval.Day);
                }
            });
    }
}
