using API.Customs.Exceptions;
using API.Data.Repositories.Interfaces;
using API.Models;
using API.Services.Interfaces;

namespace API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IProductService _productService;

        public BasketService(IBasketRepository basketRepository, IProductService productService)
        {
            _basketRepository = basketRepository;
            _productService = productService;
        }

        public async Task<Basket> GetByBuyerId(Guid buyerId)
        {
            var basket = await _basketRepository.GetByBuyerId(buyerId) ?? throw new NotFoundException($"Basket with buyerId {buyerId} was not found.");;
            return basket;
        }

        public async Task<Basket?> AddItemToBasket(Guid buyerId, Guid productId, int quantity)
        {
            var basket = await _basketRepository.GetByBuyerId(buyerId) ?? await _basketRepository.InsertAndFetch(new Basket(buyerId));

            var product = await _productService.GetById(productId);

            basket.AddItem(product, quantity);

            basket.Items.ForEach(async item => {
                if (item.productId == productId)
                {
                    if (item.Quantity != quantity)
                    {
                        await _basketItemRepository.Update(item);
                    }
                    else
                    {
                        await _basketItemRepository.Insert(item);
                    }
                }
            });

            await _basketRepository.Update(basket);

            return basket;
        }
    }
}