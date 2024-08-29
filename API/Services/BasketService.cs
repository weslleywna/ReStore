using API.Customs.Exceptions;
using API.Data.Repositories.Interfaces;
using API.Models;
using API.Services.Interfaces;

namespace API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
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
            var basket = await _basketRepository.GetByBuyerId(buyerId) ?? await CreateBasket();

            var product = await _productService.GetById(productId);

            return basket;
        }

        public async Task<Basket?> CreateBasket()
        {
            return await _basketRepository.Insert();
        }
    }
}