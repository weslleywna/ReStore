using API.Dtos;
using API.Models;

namespace API.Data.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<(IEnumerable<Product>, int)> GetAllPaginated(ProductGetAllRequestDto productGetAllRequestDto);
        Task<IEnumerable<string>> GetAllProductsBrands();
        Task<IEnumerable<string>> GetAllProductsTypes();
    }
}