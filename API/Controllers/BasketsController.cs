using API.Dtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : BaseApiController
{
    private readonly ILogger<BasketsController> _logger;

    private readonly IBasketService _basketService;

    public BasketsController(ILogger<BasketsController> logger, IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpGet]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        _ = Guid.TryParse(Request.Cookies["buyerId"], out var buyerId);
        var basket = await _basketService.GetByBuyerId(buyerId);

        if (basket == null) return NotFound();
        
        return Ok(new BasketDto(basket));
    }

    [HttpPost]
    public async Task<ActionResult> AddItemToBasket(Guid productId, int quantity)
    {
        _ = Guid.TryParse(Request.Cookies["buyerId"], out var buyerId);
        _ = await _basketService.AddItemToBasket(buyerId, productId, quantity);

        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveBasketItem(Guid productId, int quantity)
    {
        _ = Guid.TryParse(Request.Cookies["buyerId"], out var buyerId);
        await _basketService.RemoveBasketItem(buyerId, productId, quantity);
        return Ok();
    }
}
