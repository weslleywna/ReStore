using API.Models;

namespace API.Dtos
{
    public class BasketItemDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string? PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }

        public BasketItemDto(BasketItem basketItem)
        {
            ProductId = basketItem.ProductId;
            Name = basketItem.Product.Name;
            Price = basketItem.Product.Price;
            PictureUrl = basketItem.Product.PictureUrl;
            Brand = basketItem.Product.Brand;
            Type = basketItem.Product.Type;
            Quantity = basketItem.Quantity;
        }
    }
}