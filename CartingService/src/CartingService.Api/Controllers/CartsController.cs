using CartingService.Api.Models;
using CartingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartsController : ControllerBase
{
    private readonly ILogger<CartsController> _logger;
    private readonly ICartingService _cartingService;

    public CartsController(
        ILogger<CartsController> logger,
        ICartingService cartingService)
    {
        _logger = logger;
        _cartingService = cartingService;
    }

    [HttpGet("{id}")]
    public IActionResult GetCart(string id)
    {
        _logger.LogInformation("Requesting a cart with id {Id}", id);

        var cart = _cartingService.Get(id);
        if (cart is null)
        {
            _logger.LogWarning("Cart with id '{Id}' was not found", id);
            
            return NotFound(id);
        }

        return Ok(cart);
    }

    [HttpPut("{cartId}/item/{itemId}")]
    public IActionResult CreateItem(string cartId, string itemId, [FromBody] CreateItemRequest createItemRequest)
    {
        _logger.LogInformation("Create an item '{ItemId}' from the cart '{CartId}'", itemId, cartId);

        var item = createItemRequest.ToItem(itemId);
        _cartingService.CreateItem(cartId, item);

        return NoContent();
    }
    
    [HttpDelete("{cartId}/item/{itemId}")]
    public IActionResult DeleteItem(string cartId, string itemId)
    {
        _logger.LogInformation("Delete an item '{ItemId}' with the cart '{CartId}'", itemId, cartId);

        _cartingService.DeleteItem(cartId, itemId);

        return NoContent();
    }
}