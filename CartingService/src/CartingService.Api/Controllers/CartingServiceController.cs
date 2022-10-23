using CartingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartingServiceController : ControllerBase
{
    private readonly ILogger<CartingServiceController> _logger;
    private readonly ICartingService _cartingService;

    public CartingServiceController(
        ILogger<CartingServiceController> logger,
        ICartingService cartingService)
    {
        _logger = logger;
        _cartingService = cartingService;
    }

    [HttpGet("cart/id/{id}")]
    public IActionResult GetCart(string id)
    {
        _logger.LogInformation("Requesting a cart with id {Id}", id);

        var cart = _cartingService.Get(id);
        if (cart is null)
        {
            return NotFound($"Cart with id '{id}' was not found'");
        }

        return Ok(cart);
    }
}