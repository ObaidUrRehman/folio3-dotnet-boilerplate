﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Folio3.Sbp.Data.AuditLogging.Extensions
{
    public static class AuditedRepoExtensions
    {
        public static IServiceCollection ConfigureAuditLogging<T>(
            this IServiceCollection services, string auditLogDbConnectionString) where T : IAuditMetaData
        {
            services
                .AddScoped(typeof(IAuditMetaData), typeof(T))
                ;

            services.AddDbContext<AuditLogDbContext>(options =>
                options.UseSqlServer(auditLogDbConnectionString,
                    b => b.MigrationsAssembly("Folio3.Sbp.Data")));


            return services;
        }
    }
}