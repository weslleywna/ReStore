using API.Data.Config;
using API.Data.Repositories.Base;
using API.Data.Repositories.Interfaces;
using API.Models;
using Dapper;

namespace API.Data.Repositories
{
    public class BasketRepository : DbReStoreRepositoryBase<Basket>, IBasketRepository
    {
        public BasketRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

        public async Task<Basket?> GetByBuyerId(Guid buyerId)
        {
            const string sql = @"SELECT 
                                    b.id AS ""Id"",
                                    b.buyer_id AS ""BuyerId"",
                                    bi.id AS ""Id"",
                                    bi.quantity AS ""Quantity"",
                                    bi.basket_id AS ""BasketId"",
                                    bi.product_id AS ""ProductId"",
                                    p.id AS ""Id"",
                                    p.name AS ""Name"",
                                    p.brand AS ""Brand"",
                                    p.price AS ""Price"",
                                    p.quantity_in_stock AS ""QuantityInStock""
                                FROM baskets b
                                LEFT JOIN basket_items bi ON b.id = bi.basket_id
                                LEFT JOIN products p ON bi.product_id = p.id
                                WHERE b.buyer_id = @BuyerId";

            var basketDictionary = new Dictionary<Guid, Basket>();

            var result = await DbConnection.QueryAsync<Basket, BasketItem, Product, Basket>(
                sql,
                (basket, item, product) =>
                {
                    if (!basketDictionary.TryGetValue(basket.Id, out var basketEntry))
                    {
                        basketEntry = basket;
                        basketDictionary.Add(basket.Id, basketEntry);
                    }

                    if (item != null)
                    {
                        item.Product = product;
                        basketEntry.Items.Add(item);
                    }

                    return basketEntry;
                },
                new { BuyerId = buyerId },
                splitOn: "Id, Id"
            );

            return basketDictionary.Values.SingleOrDefault();
        }

        public async Task<Basket?> Insert()
        {
            throw new NotImplementedException();
        }
    }
}