using CartingService.Api.Models;
using CartingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Api.Controllers.v1;

/// <summary>
/// API for carting service managing
/// </summary>
[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
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
    /// Gets a cart with all the items
    /// </summary>
    /// <param name="id">A cart unique id</param>
    /// <returns>A cart model with all the items</returns>
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

        return Ok(cart);
    }

    /// <summary>
    /// Puts an item with an itemId into a cart with a cartId 
    /// </summary>
    /// <param name="cartId">A cart unique id</param>
    /// <param name="itemId">An item unique id</param>
    /// <param name="createItemRequest">A request with item's params</param>
    /// <remarks>
    /// If a cart with a specified cartId doesn't exist this call will create it and put the item to it
    /// </remarks>
    /// <response code='201'>The item has been put into the cart (the cart has been created if hadn't existed before)</response>
    /// <response code='400'>The item has invalid params, or the cart has already had this item</response>
    [HttpPut("{cartId}/item/{itemId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public IActionResult PutItem(string cartId, string itemId, [FromBody] CreateItemRequest createItemRequest)
    {
        _logger.LogInformation("Puts an item with an {ItemId} into a cart with a {CartId} ", itemId, cartId);

        var item = createItemRequest.ToItem(itemId);
        _cartingService.PutItem(cartId, item);

        return CreatedAtAction(nameof(PutItem), new { cartId, itemId });
    }
    
    /// <summary>
    /// Delete an item with an itemId from a cart with a cartId
    /// </summary>
    /// <param name="cartId">A cart unique id</param>
    /// <param name="itemId">An item unique id</param>
    /// /// <response code='204'>The item was removed from the cart</response>
    /// /// <response code='404'>The item or the cart was not found</response>
    [HttpDelete("{cartId}/item/{itemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public IActionResult DeleteItem(string cartId, string itemId)
    {
        _logger.LogInformation("Delete an item '{ItemId}' with the cart '{CartId}'", itemId, cartId);

        _cartingService.DeleteItem(cartId, itemId);

        return NoContent();
    }
}