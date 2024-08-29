using API.Data.Config;
using API.Data.Repositories.Base;
using API.Data.Repositories.Interfaces;
using API.Models;
using Dapper;

namespace API.Data.Repositories
{
    public class ProductRepository : DbReStoreRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }
    }
}