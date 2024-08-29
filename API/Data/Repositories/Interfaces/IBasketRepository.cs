using API.Models;

namespace API.Data.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket?> GetByBuyerId(Guid buyerId);
        Task<Basket?> Insert();
    }
}