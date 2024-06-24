using API.Data.Repositories.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseApiController
{
    private readonly ILogger<ProductsController> _logger;

    private readonly IProductRepository _productRepository;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _productRepository.GetAll();
        
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var products = await _productRepository.GetById(id);

        if (products == null)
        {
            return NotFound();
        }
        
        return Ok(products);
    }
}
