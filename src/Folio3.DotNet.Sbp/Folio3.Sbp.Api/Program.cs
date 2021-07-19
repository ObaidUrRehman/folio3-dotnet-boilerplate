using Folio3.Sbp.Common.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Folio3.Sbp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppConfig.ConfigureSerilog();
            try
            {
                Log.Information("Starting Folio3.Sbp.Api");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((context, builder) =>
                            builder.CombineSettings(
                                context.HostingEnvironment.EnvironmentName,
                                context.HostingEnvironment.ContentRootPath,
                                @".."))
                        .UseStartup<Startup>();
                });
        }
    }
}