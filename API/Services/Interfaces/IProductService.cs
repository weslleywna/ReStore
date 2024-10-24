using API.Dtos;
using API.Models;
using API.Utils;

namespace API.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedList<Product>> GetAllPaginated(ProductGetAllRequestDto productGetAllRequestDto);
        Task<Product> GetById(Guid id);
        Task<IEnumerable<string>> GetAllProductsBrands();
        Task<IEnumerable<string>> GetAllProductsTypes();
    }
}