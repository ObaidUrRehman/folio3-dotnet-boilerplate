﻿using System;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Folio3.Sbp.Api.Swagger
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Folio3.Sbp.Api", 
                    Version = "v1", 
                    Description = "Folio3.Sbp.Api Sample Api. Use this placeholder text to add description about your Api."
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Sbp API",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Folio3.Sbp.Api.xml");
                c.IncludeXmlComments(filePath);
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Folio3.Sbp.Api v1");
                c.EnableDeepLinking();
                c.DocExpansion(DocExpansion.List);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DisplayRequestDuration();
                c.EnableFilter();
            });
            return app;
        }
    }
}