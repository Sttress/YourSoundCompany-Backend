using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sync;
using Sync.Services;
using YourSoundCompany.RelationalData;
using YourSoundCompany.RelationalData.Repository;
using YourSoundCompany.Worker.Jobs;
using YourSoundCompnay.RelationalData;
using YourSoundCompnay.RelationalData.Repository;

namespace YourSoundCompany.Worker
{
    public static class ConfigureDependencyInjection
    {
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration) =>
        services.ConfigureService()
                .ConfigureRepository(configuration);


        private static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IUserSyncService, UserSyncService>();

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
