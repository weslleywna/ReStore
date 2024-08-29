using API.Customs.Exceptions;
using API.Data.Repositories.Interfaces;
using API.Models;
using API.Services.Interfaces;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _productRepository.GetById(id) ?? throw new NotFoundException($"Product with id {id} was not found.");

            return product;
        }
    }
}