using API.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace API.Data.Mappers
{
    public class ProductMap : DommelEntityMap<Product>
    {
        public ProductMap()
        {
            ToTable("products");
            Map(x => x.Id).ToColumn("id").IsKey();           
            Map(x => x.Brand).ToColumn("brand");
            Map(x => x.Name).ToColumn("name");
            Map(x => x.Price).ToColumn("price");
            Map(x => x.QuantityInStock).ToColumn("quantity_in_stock");
            Map(x => x.Description).ToColumn("description");
            Map(x => x.PictureUrl).ToColumn("picture_url");
        }
    }
}