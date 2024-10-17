using System.Text;
using API.Data.Repositories.Base;
using API.Data.Repositories.Interfaces;
using API.Dtos;
using API.Enums;
using API.Models;
using Dapper;

namespace API.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbSessionReStoreRepositoryBase dbSession) : base(dbSession) { }

        public async Task<(IEnumerable<Product>, int)> GetAllPaginated(ProductGetAllRequestDto productGetAllRequestDto)
        {
            var sqlBuilder = new StringBuilder();
            var countSqlBuilder = new StringBuilder();

            sqlBuilder.Append("SELECT * FROM products WHERE 1=1 ");
            countSqlBuilder.Append("SELECT COUNT(*) FROM products WHERE 1=1 ");

            if (!string.IsNullOrEmpty(productGetAllRequestDto.SearchTerm))
            {
                sqlBuilder.Append("AND (name ILIKE @SearchTerm OR description ILIKE @SearchTerm) ");
                countSqlBuilder.Append("AND (name ILIKE @SearchTerm OR description ILIKE @SearchTerm) ");
            }

            if (productGetAllRequestDto.Brands != null && productGetAllRequestDto.Brands.Count != 0)
            {
                sqlBuilder.Append("AND brand = ANY(@Brands) ");
                countSqlBuilder.Append("AND brand = ANY(@Brands) ");
            }

            if (productGetAllRequestDto.Types != null && productGetAllRequestDto.Types.Count != 0)
            {
                sqlBuilder.Append("AND type = ANY(@Types) ");
                countSqlBuilder.Append("AND type = ANY(@Types) ");
            }

            switch (productGetAllRequestDto.OrderBy)
            {
                case ProductOrder.PriceAsc:
                    sqlBuilder.Append("ORDER BY price ASC ");
                    break;
                case ProductOrder.PriceDesc:
                    sqlBuilder.Append("ORDER BY price DESC ");
                    break;
                case ProductOrder.NameAsc:
                    sqlBuilder.Append("ORDER BY name ASC ");
                    break;
                case ProductOrder.NameDesc:
                    sqlBuilder.Append("ORDER BY name DESC ");
                    break;
                case ProductOrder.UpdatedAtAsc:
                    sqlBuilder.Append("ORDER BY updated_at ASC ");
                    break;
                case ProductOrder.UpdatedAtDesc:
                    sqlBuilder.Append("ORDER BY updated_at DESC ");
                    break;
                default:
                    sqlBuilder.Append("ORDER BY created_at DESC ");
                    break;
            }

            var offset = (productGetAllRequestDto.PageNumber - 1) * productGetAllRequestDto.PageSize;
            sqlBuilder.Append("LIMIT @PageSize OFFSET @Offset;");

            var products = await _session.Connection.QueryAsync<Product>(
                sqlBuilder.ToString(),
                new
                {
                    SearchTerm = $"%{productGetAllRequestDto.SearchTerm}%",
                    productGetAllRequestDto.Brands,
                    productGetAllRequestDto.Types,
                    productGetAllRequestDto.PageSize,
                    Offset = offset
                }
            );

            var totalRecords = await _session.Connection.ExecuteScalarAsync<int>(
                countSqlBuilder.ToString(),
                new
                {
                    SearchTerm = $"%{productGetAllRequestDto.SearchTerm}%",
                    productGetAllRequestDto.Brands,
                    productGetAllRequestDto.Types
                }
            );

            return (products, totalRecords);
        }
    }
}