using API.Data.Factory;
using API.Data.Repositories;
using API.Data.Repositories.Interfaces;

namespace API.Data.Config.Extensions
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionDict = new Dictionary<DatabaseConnectionNameEnum, string?>
            {
                { DatabaseConnectionNameEnum.DbReStore, configuration.GetConnectionString("dbReStore") },
                // { DatabaseConnectionName.Connection2, this.Configuration.GetConnectionString("dbConnection2") }
            };

            // Inject this dict
            services.AddSingleton<IDictionary<DatabaseConnectionNameEnum, string?>>(connectionDict);

            // Inject the factory
            services.AddTransient<IDbConnectionFactory, DapperDbConnectionFactory>();

            // Register your regular repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
        }
    }
}