using Backend_API.Repositories;
using Backend_API.Repositories.RepositoryInterfaces;

namespace Backend_API.ServiceRegistrations
{
    public static class RepositoryServiceRegistration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IDisciplineRepository, DisciplineRepository>();
            services.AddScoped<IDirectionRepository, DirectionRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();

        }
    }
}
