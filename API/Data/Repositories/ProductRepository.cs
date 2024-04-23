using API.Data.Config;
using API.Data.Repositories.Base;
using API.Data.Repositories.Interfaces;
using API.Models;
using Dapper;

namespace API.Data.Repositories
{
    public class ProductRepository : DbReStoreRepositoryBase, IProductRepository
    {
        public ProductRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }
        
        public IEnumerable<Product> GetAll()
        {
            const string sql = @"SELECT * FROM product";

            // No need to use using statement. Dapper will automatically
            // open, close and dispose the connection for you.
            return base.DbConnection.Query<Product>(sql);
        }
    }
}