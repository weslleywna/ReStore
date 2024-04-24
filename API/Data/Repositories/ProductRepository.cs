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
        
        public async Task<IEnumerable<Product>> GetAll()
        {
            const string sql = @"SELECT id as ""Id"",
                                        brand as ""Brand"",
                                        name as ""Name"",
                                        type as ""Type"",
                                        price as ""Price"",
                                        quantity_in_stock as ""QuantityInStock"",
                                        description as ""Description"",
                                        picture_url as ""PictureUrl""
                                FROM products";

            return await DbConnection.QueryAsync<Product>(sql);
        }

        public async Task<Product?> GetById(Guid id)
        {
            const string sql = @"SELECT id as ""Id"",
                                        brand as ""Brand"",
                                        name as ""Name"",
                                        type as ""Type"",
                                        price as ""Price"",
                                        quantity_in_stock as ""QuantityInStock"",
                                        description as ""Description"",
                                        picture_url as ""PictureUrl""
                                FROM products
                                WHERE id = @Id";

            return await DbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }
    }
}