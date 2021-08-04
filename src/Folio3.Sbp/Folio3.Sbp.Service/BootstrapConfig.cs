using AutoMapper;
using Folio3.Sbp.Common.Email;
using Folio3.Sbp.Common.Settings;
using Folio3.Sbp.Service.Common.Services;
using Folio3.Sbp.Service.School.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Folio3.Sbp.Service
{
    public static class BootstrapConfig
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            return services
                // configure strongly typed settings object
                .Configure<MailgunSettings>(configuration.GetSection("Mailgun"))


                .AddSingleton(mappingConfig.CreateMapper())
                .AddScoped<JwtTokenService>()
                .AddScoped<IEmailSender, MailgunEmailSender>()
                .AddScoped<IExtendedEmailSender, MailgunEmailSender>()


                .AddScoped<UserService>()
                .AddScoped<StudentService>()

                ;
        }
    }
}