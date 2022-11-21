using CartingService.Api.Models;
using CartingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Api.Controllers.v2;

/// <summary>
/// Advanced API for carting service managing
/// </summary>
[ApiController]
[Route("[controller]")]
[ApiVersion("2.0")]
[Produces("application/json")]
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

    /// <summary>
    /// Gets all the items from the cart
    /// </summary>
    /// <param name="id">A cart unique id</param>
    /// <returns>A new cart model with all the items</returns>
    /// <response code='200'>The cart with all the items returns</response>
    /// <response code='404'>The cart was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetCart(string id)
    {
        _logger.LogInformation("Requesting a cart with id {Id}", id);

        var cart = _cartingService.Get(id);
        if (cart is null)
        {
            _logger.LogWarning("Cart with id '{Id}' was not found", id);
            
            return NotFound(id);
        }

        return Ok(new CartItemsResponse(cart.Items));
    }
}