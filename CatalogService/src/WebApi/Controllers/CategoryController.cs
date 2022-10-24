using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet("category/id/{id}")]
    public IActionResult Get(string id)
    {
        _logger.LogInformation("Getting a category with id {Id}", id);
        
        return Ok();
    }
}