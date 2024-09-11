using API.Data.Factory;
using API.Data.Repositories;
using API.Data.Repositories.Base;
using API.Data.Repositories.Interfaces;
using API.Data.UoW;

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
            
            services.AddTransient<IDbConnectionFactory, DapperDbConnectionFactory>();

            services.AddScoped(provider =>
            {
                var dbConnectionFactory = provider.GetRequiredService<IDbConnectionFactory>();
                var databaseEnum = DatabaseConnectionNameEnum.DbReStore;
                return new DbSessionReStoreRepositoryBase(dbConnectionFactory, databaseEnum);
            });

            // Register your regular repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        }
    }
}