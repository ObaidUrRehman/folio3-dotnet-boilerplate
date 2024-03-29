﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Folio3.Sbp.Common.Config
{
    public static class AppConfig
    {
        public static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
                .CreateLogger();
        }

        /// <summary>
        ///     Configure the shared Application settings by combining all the config files together
        /// </summary>
        /// <param name="config">Configuration Builder</param>
        /// <param name="environment">Name of the current environment (e.g. "Development")</param>
        /// <param name="contentRootPath">Root path for static content files</param>
        /// <param name="shareFolderRelatedPath">Relative path from content root where settings files live</param>
        /// <returns></returns>
        public static IConfigurationBuilder CombineSettings(
            this IConfigurationBuilder config,
            string environment,
            string contentRootPath,
            string shareFolderRelatedPath)
        {
            // find the shared folder in the parent folder
            var sharedFolder = Path.Combine(contentRootPath, shareFolderRelatedPath);

            foreach (var path in new[]
            {
                // load the SharedSettings first, so that appsettings.json can override it if it needs to
                // e.g. /Sylfph.Web/SharedSettings.json
                Path.Combine(sharedFolder, "SharedSettings.json"),

                // load environment specific shared settings
                // e.g. /Sylfph.Web/SharedSettings.Development.json
                Path.Combine(sharedFolder, $"SharedSettings.{environment}.json"),

                // load app settings
                // e.g. /Sylfph.Web/Sylfph.Admin/appsettings.json
                "appsettings.json",

                // load app environment specific settings
                // e.g. /Sylfph.Web/Sylfph.Admin/appsettings.Development.json
                $"appsettings.{environment}.json",

                // load master machine config
                // e.g. /Sylfph.Web/MasterMachineSettings.json
                Path.Combine(sharedFolder, "MasterMachineSettings.json"),

                // load machine specific shared settings last so it can override any setting
                // e.g. /Sylfph.Web/Settings.MyCoolComputer.json
                Path.Combine(sharedFolder, $"Settings.{Environment.MachineName}.json")
            })
                config.AddJsonFile(path, true);

            config.AddEnvironmentVariables();

            return config;
        }
    }
}