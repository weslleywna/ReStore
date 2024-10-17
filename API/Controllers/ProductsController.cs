using System.Text.Json;
using API.Dtos;
using API.Enums;
using API.Models;
using API.Services.Interfaces;
using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseApiController
{
    private readonly ILogger<ProductsController> _logger;

    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<Product>>> Get([FromQuery] ProductGetAllRequestDto productGetAllRequestDto)
    {
        var products = await _productService.GetAllPaginated(productGetAllRequestDto);

        Response.Headers.Append("Pagination", JsonSerializer.Serialize(products.PaginationMetaData));
        
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var products = await _productService.GetById(id);
        
        return Ok(products);
    }
}
