using API.Data.Config;
using API.Data.Repositories.Base;
using API.Data.Repositories.Interfaces;
using API.Models;
using Dapper;

namespace API.Data.Repositories
{
    public class BasketItemRepository : DbReStoreRepositoryBase<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }
    }
}