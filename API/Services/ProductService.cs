using API.Customs.Exceptions;
using API.Data.Repositories.Interfaces;
using API.Dtos;
using API.Models;
using API.Services.Interfaces;
using API.Utils;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedList<Product>> GetAllPaginated(ProductGetAllRequestDto productGetAllRequestDto)
        {
            var (products, totalItems) = await _productRepository.GetAllPaginated(productGetAllRequestDto);
            return PagedList<Product>.ToPagedList(products, totalItems, productGetAllRequestDto.PageNumber, productGetAllRequestDto.PageSize); 
        }

        public async Task<IEnumerable<string>> GetAllProductsBrands()
        {
            var brands = await _productRepository.GetAllProductsBrands();

            return brands;
        }

        public async Task<IEnumerable<string>> GetAllProductsTypes()
        {
            var types = await _productRepository.GetAllProductsTypes();

            return types;
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _productRepository.GetById(id) ?? throw new NotFoundException($"Product with id {id} was not found.");

            return product;
        }
    }
}