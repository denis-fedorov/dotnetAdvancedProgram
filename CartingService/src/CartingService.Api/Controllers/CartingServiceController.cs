using Microsoft.AspNetCore.Mvc;

namespace CartingService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartingServiceController : ControllerBase
{
    private readonly ILogger<CartingServiceController> _logger;

    public CartingServiceController(ILogger<CartingServiceController> logger)
    {
        _logger = logger;
    }

    [HttpGet("cart/id/{id}")]
    public IActionResult GetCart(string id)
    {
        _logger.LogInformation("Requesting a cart with id {Id}", id);
        
        if (id == "1")
        {
            return Ok(id);
        }

        return NotFound(id);
    }
}