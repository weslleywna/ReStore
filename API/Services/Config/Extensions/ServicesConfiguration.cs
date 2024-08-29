using API.Services.Interfaces;

namespace API.Services.Config.Extensions
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Register your services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
        }
    }
}