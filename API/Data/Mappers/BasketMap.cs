using API.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace API.Data.Mappers
{
    public class BasketMap : DommelEntityMap<Basket>
    {
        public BasketMap()
        {
            ToTable("baskets");
            Map(x => x.Id).ToColumn("id").IsKey();  
            Map(x => x.CreatedAt).ToColumn("created_at"); 
            Map(x => x.UpdatedAt).ToColumn("updated_at");          
            Map(x => x.BuyerId).ToColumn("buyer_id");
        }
    }
}