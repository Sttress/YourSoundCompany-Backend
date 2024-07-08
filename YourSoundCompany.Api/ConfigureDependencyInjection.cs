
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
using YourSoundCompany.EmailService;
using YourSoundCompany.EmailService.Service;
using YourSoundCompany.Templates;
using YourSoundCompany.Templates.Service;
using YourSoundCompany.CacheService.Service;
using YourSoundCompany.RelationalData;
using YourSoundCompany.RelationalData.Repository;
using YourSoundCompany.IntegrationSpotify;
using YourSoundCompany.IntegrationSpotify.Service;
using YourSoundCompany.Business.Validation.User;

namespace YourSoundCompnay.Api
{
    public static class ConfigureDependencyInjection
    {
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration) =>
        services.ConfigureService()
            .ConfigureRepository(configuration)
            .ConfigureValidation();


        private static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMap).Assembly);
            services.AddHttpClient();

            services.AddScoped<IUtilsService,UtilsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISpotifyService, SpotifyService>();
            services.AddScoped<ISpotifyCacheService, SpotifyCacheService>();
            services.AddScoped<ISpotifyAuthService, SpotifyAuthService>();
            services.AddScoped<ITemplateEmailService, TemplateEmailService>();
            services.AddScoped<ISendEmailService,SendEmailService>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddSingleton<ICacheService, CacheService>();


            return services;
        }



        private static IServiceCollection ConfigureValidation(this IServiceCollection services)
        {
            services.AddTransient<UserCreateValidator>();
            services.AddTransient<UserUpdateValidator>();
            services.AddTransient<UserRecoveryPasswordValidator>();
            return services;
        }

        private static IServiceCollection ConfigureRepository(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DB");


            services.AddDbContext<ContextDB>(
                opt => opt
                    .UseNpgsql(connectionString)
            );

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
