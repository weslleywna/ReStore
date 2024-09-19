using API.Customs.Exceptions;
using API.Data.Repositories.Interfaces;
using API.Data.UoW;
using API.Models;
using API.Services.Interfaces;

namespace API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;

        public BasketService(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IProductService productService, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _productService = productService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Basket> GetByBuyerId(Guid buyerId)
        {
            var basket = await _basketRepository.GetByBuyerId(buyerId) ?? throw new NotFoundException($"Basket with buyerId {buyerId} was not found.");;
            return basket;
        }

        public async Task<Basket> AddItemToBasket(Guid buyerId, Guid productId, int quantity)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var basket = await _basketRepository.GetByBuyerId(buyerId) ?? await _basketRepository.InsertAndFetch(new Basket(buyerId));
                var product = await _productService.GetById(productId);
                basket.AddItem(product, quantity);

                var existingItem = basket.Items.FirstOrDefault(item => item.ProductId == productId);
                if (existingItem?.Quantity != quantity) await _basketItemRepository.Update(existingItem!);
                else await _basketItemRepository.Insert(existingItem);

                _unitOfWork.Commit();
                return basket;
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task RemoveBasketItem(Guid buyerId, Guid productId, int quantity)
        {
            var basket = await GetByBuyerId(buyerId);
            basket.RemoveItem(productId, quantity);
            
            var existingItem = basket.Items.FirstOrDefault(item => item.ProductId == productId) ?? throw new NotFoundException($"Product with productId {productId} was not found in basket.");
            
            if (existingItem!.Quantity > 0) await _basketItemRepository.Update(existingItem!);
            else await _basketItemRepository.Delete(existingItem!.Id);
        }
    }
}