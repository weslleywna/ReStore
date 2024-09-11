using API.Data.Repositories.Base;
using API.Data.Repositories.Interfaces;
using API.Models;

namespace API.Data.Repositories
{
    public class BasketItemRepository : RepositoryBase<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(DbSessionReStoreRepositoryBase dbSession) : base(dbSession) { }
    }
}