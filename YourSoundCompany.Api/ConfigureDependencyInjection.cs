
using YourSoundCompnay.RelationalData;
using YourSoundCompnay.RelationalData.Repository;
using Microsoft.EntityFrameworkCore;
using YourSoundCompnay.Business.Map;
using YourSoundCompnay.Business.Validation.User;
using YourSoundCompnay.SesseionService;
using YourSoundCompnay.SesseionService.Service;
using YourSoundCompnay.Business.Service;
using YourSoundCompnay.Business;
using YourSoundCompany.Business;
using YourSoundCompany.Business.Service;

namespace YourSoundCompnay.Api
{
    public static class ConfigureDependencyInjection
    {
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration) =>
        services.ConfigureService(configuration)
            .ConfigureRepository(configuration)
            //.ConfigureIntegrationSpotify()
            .ConfigureValidation();


        private static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(UserMap).Assembly);
            services.AddHttpClient();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISpotifyService, SpotifyService>();

            return services;
        }

        //private static IServiceCollection ConfigureIntegrationSpotify(this IServiceCollection services)
        //{
        //    services.AddHttpClient();
        //    services.AddScoped<ISpotifyService, SpotifyService>();
        //    return services;

        //}

        private static IServiceCollection ConfigureValidation(this IServiceCollection services)
        {
            services.AddTransient<UserCreateValidator>();
            services.AddTransient<UserUpdateValidator>();

            return services;
        }

        private static IServiceCollection ConfigureRepository(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DB");


            services.AddDbContext<ContextDB>(
                opt => opt
                    .UseNpgsql(connectionString)
            );

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
