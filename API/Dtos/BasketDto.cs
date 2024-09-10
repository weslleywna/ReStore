using API.Models;

namespace API.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; } = [];

        public BasketDto(Basket basket)
        {
            Id = basket.Id;
            BuyerId = basket.BuyerId;
            Items = basket.Items.Select(item => new BasketItemDto(item)).ToList();
        }
    }
}