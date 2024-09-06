using API.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace API.Data.Mappers
{
    public class BasketMap : DommelEntityMap<BasketMap>
    {
        public BasketMap()
        {
            ToTable("baskets");
            Map(x => x.Id).ToColumn("id").IsKey();           
            Map(x => x.BuyerId).ToColumn("buyer_id");
        }
    }
}