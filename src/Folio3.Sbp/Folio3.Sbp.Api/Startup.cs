using Folio3.Sbp.Api.Attributes;
using Folio3.Sbp.Api.Middleware;
using Folio3.Sbp.Api.Provider;
using Folio3.Sbp.Api.Swagger;
using Folio3.Sbp.Data.AuditLogging.Extensions;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Service;
using Folio3.Sbp.Service.Background;
using Folio3.Sbp.Service.Claims;
using Folio3.Sbp.Service.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Folio3.Sbp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<SchoolDbContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:school"]))
                .ConfigureAuditLogging<AuditMetaData>(Configuration["ConnectionStrings:auditLog"])
                .RegisterApplicationServices()
                .AddScoped<ValidateModelAttribute>()
                .AddHttpContextAccessor()
                .AddScoped<IUserClaims, UserClaims>()
                .AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SchoolDbContext>()
                ;

            services.ConfigureSwagger();

            services.AddControllers();

            services.ConfigureSampleBackgroundJob();

            services.ConfigureJwtBearerAuth(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                    await GenericApiErrorHandler.HandleErrorAsync(context, logger, env.IsDevelopment()));
            });

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.ConfigureSwaggerUi();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}