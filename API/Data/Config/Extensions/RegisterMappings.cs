using API.Data.Mappers;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace API.Data.Config.Extensions
{
    public static class RegisterMappings
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ProductMap());
                config.AddMap(new BasketMap());
                config.AddMap(new BasketItemMap());
                config.ForDommel();
            });
        }
    }
}