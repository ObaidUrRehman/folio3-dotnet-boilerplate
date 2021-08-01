using Folio3.Sbp.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Folio3.Sbp.Service.Common.Extensions
{
	public static class JwtExtension
	{
        public static IServiceCollection ConfigureJwtBearerAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("JwtTokenSettings");
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

            return services;
        }
    }
}