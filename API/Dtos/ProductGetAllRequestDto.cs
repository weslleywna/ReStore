using API.Enums;
using API.Utils;

namespace API.Dtos
{
    public class ProductGetAllRequestDto : PaginationParams
    {
        public ProductOrder OrderBy { get; set; }
        public string? SearchTerm { get; set; }
        public string? Types { get; set; }
        public string? Brands { get; set; }
    }
}