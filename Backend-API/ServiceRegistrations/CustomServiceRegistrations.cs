using Backend_API.Services;
using Backend_API.Services.ServiceInterfaces;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Backend_API.ServiceRegistrations
{
    public static class CustomServiceRegistrations
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAvatarService, AvatarService>();
        }
    }
}
