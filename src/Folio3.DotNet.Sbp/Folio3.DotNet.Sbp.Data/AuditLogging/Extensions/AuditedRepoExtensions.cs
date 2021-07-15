using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Folio3.DotNet.Sbp.Data.AuditLogging.Extensions
{
    public static class AuditedRepoExtensions
    {
        public static IServiceCollection ConfigureAuditLogging<T> (
            this IServiceCollection services, string auditLogDbConnectionString) where T : IAuditMetaData
        {
            services
                .AddScoped<AuditLogGenerator>()
                .AddScoped<AuditLogger>()
                .AddScoped(typeof(IAuditMetaData), typeof(T))
                ;

            services.AddDbContext<AuditLogDbContext>(options =>
                options.UseSqlServer(auditLogDbConnectionString, 
                    b => b.MigrationsAssembly("Folio3.DotNet.Sbp.Data")));


            return services;
        }

    }
}
