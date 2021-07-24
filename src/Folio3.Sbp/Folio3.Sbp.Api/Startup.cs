using System;
using System.Text;
using Folio3.Sbp.Api.Attributes;
using Folio3.Sbp.Api.Middleware;
using Folio3.Sbp.Api.Provider;
using Folio3.Sbp.Api.Swagger;
using Folio3.Sbp.Common.Settings;
using Folio3.Sbp.Data.AuditLogging.Extensions;
using Folio3.Sbp.Data.School;
using Folio3.Sbp.Data.School.Entities;
using Folio3.Sbp.Service;
using Folio3.Sbp.Service.Background;
using Folio3.Sbp.Service.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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


            // TODO: Move JWT configure to separate class like the AddBackgroundTaskQueue
            var section = Configuration.GetSection("JwtTokenSettings");
            services.Configure<JwtTokenSettings>(section);
            var jwtTokenSettings = section.Get<JwtTokenSettings>();

            // JWT Bearer Auth
            services
                .AddAuthentication(c =>
                {
                    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(c =>
                {
                    c.RequireHttpsMetadata = true;
                    c.SaveToken = true;
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtTokenSettings.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenSettings.Secret)),
                        ValidAudience = jwtTokenSettings.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
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