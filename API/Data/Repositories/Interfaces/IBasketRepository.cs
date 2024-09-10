using API.Models;

namespace API.Data.Repositories.Interfaces
{
    public interface IBasketRepository : IRepositoryBase<Basket>
    {
        Task<Basket?> GetByBuyerId(Guid buyerId);
    }
}