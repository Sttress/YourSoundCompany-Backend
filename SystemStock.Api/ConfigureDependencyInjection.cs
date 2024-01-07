
using SystemStock.RelationalData;
using SystemStock.RelationalData.Repository;
using Microsoft.EntityFrameworkCore;
using SystemStock.Business.Map;
using SystemStock.Business.Service;
using SystemStock.Business.Service.User;
using SystemStock.Business.Service.Store;
using SystemStock.Business.Service.Authentication;
using SystemStock.Business.Validation.User;
using SystemStock.Business.Validation.Store;
using SystemStock.Business.Validation.Category;
using SystemStock.Business.Service.Category;

namespace SystemStock.Api
{
    public static class ConfigureDependencyInjection
    {
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration) =>
        services.ConfigureService(configuration)
            .ConfigureRepository(configuration)
            .ConfigureValidation();


        private static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(UserMap).Assembly);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }

        private static IServiceCollection ConfigureValidation(this IServiceCollection services)
        {
            services.AddTransient<UserCreateValidator>();
            services.AddTransient<UserUpdateValidator>();
            services.AddTransient<StoreCreateValidator>();
            services.AddTransient<CategoryCreateValidator>();

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
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
