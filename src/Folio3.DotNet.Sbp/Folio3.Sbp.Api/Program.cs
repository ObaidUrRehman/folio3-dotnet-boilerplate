using Folio3.Sbp.Common.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((context, builder) =>
                            builder.CombineSettings(
                                context.HostingEnvironment.EnvironmentName,
                                context.HostingEnvironment.ContentRootPath,
                                @".."))
                        .ConfigureLogging((c, l) => { l.AddConfiguration(c.Configuration); })
                        .UseStartup<Startup>();
                });
        }
    }
}