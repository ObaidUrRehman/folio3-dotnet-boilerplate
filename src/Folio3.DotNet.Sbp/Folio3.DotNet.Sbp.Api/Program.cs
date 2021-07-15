using Folio3.DotNet.Sbp.Common.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Folio3.DotNet.Sbp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((WebHostBuilderContext context, IConfigurationBuilder builder) =>
                            builder.CombineSettings(
                                environment: context.HostingEnvironment.EnvironmentName,
                                contentRootPath: context.HostingEnvironment.ContentRootPath,
                                shareFolderRelatedPath: @".."))
                        .ConfigureLogging((c, l) =>
                        {
                            l.AddConfiguration(c.Configuration);
                        })
                        .UseStartup<Startup>();
                });
    }
}
