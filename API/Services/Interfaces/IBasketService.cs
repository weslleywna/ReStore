using API.Models;

namespace API.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetByBuyerId(Guid buyerId);
        Task<Basket> AddItemToBasket(Guid buyerId, Guid productId, int quantity);
        Task RemoveBasketItem(Guid buyerId, Guid productId, int quantity);
    }
}