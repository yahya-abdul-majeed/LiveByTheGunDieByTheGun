using Backend_API.Data;
using Backend_API.Models.UserModels;
using Backend_API.Options;
using Backend_API.Repositories;
using Backend_API.Repositories.RepositoryInterfaces;
using Backend_API.Services;
using Backend_API.Services.ServiceInterfaces;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend_API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAvatarService, AvatarService>();
            return services;
        }
        public static IServiceCollection AddHangfireSupport(this IServiceCollection services, IConfiguration config)
        {
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(config.GetConnectionString("hangfireconnection"))
            );

            services.AddHangfireServer();
            return services;

        }
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentityCore<Student>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentityCore<Teacher>().AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IDisciplineRepository, DisciplineRepository>();
            services.AddScoped<IDirectionRepository, DirectionRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;

        }

        public static IServiceCollection AddJWTSupport(this IServiceCollection services, IConfiguration config)
        {
            var jwtOptions = ConfigurationBinder.Get<JWTOptions>(config);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.ValidAudience,
                    ValidIssuer = jwtOptions.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                };
            });
            return services;
        }
        
    }
}
