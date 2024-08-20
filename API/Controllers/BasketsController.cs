using API.Data.Repositories.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : BaseApiController
{
    private readonly ILogger<BasketsController> _logger;

    private readonly IBasketRepository _basketRepository;

    public BasketsController(ILogger<BasketsController> logger, IBasketRepository basketRepository)
    {
        _logger = logger;
        _basketRepository = basketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasket()
    {
        _ = Guid.TryParse(Request.Cookies["buyerId"], out var buyerId);
        var basket = await _basketRepository.GetByBuyerId(buyerId);

        if (basket == null) return NotFound();
        
        return Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
    {
        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
    {
        return Ok();
    }
}
