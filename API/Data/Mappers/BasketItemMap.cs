using API.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace API.Data.Mappers
{
    public class BasketItemMap : DommelEntityMap<BasketItem>
    {
        public BasketItemMap()
        {
            ToTable("basket_items");
            Map(x => x.Id).ToColumn("id").IsKey();           
            Map(x => x.Quantity).ToColumn("quantity");
            Map(x => x.ProductId).ToColumn("product_id");
            Map(x => x.BasketId).ToColumn("basket_id");
        }
    }
}