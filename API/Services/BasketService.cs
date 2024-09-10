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

        public BasketService(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IProductService productService)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
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

            var existingItem = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem?.Quantity != quantity) await _basketItemRepository.Update(existingItem!);
            else await _basketItemRepository.Insert(existingItem);

            return basket;
        }
    }
}