using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

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

    [HttpGet("category")]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Getting all categories");

        return Ok();
    }

    [HttpGet("category/name/{name}")]
    public IActionResult Get(string name)
    {
        _logger.LogInformation("Getting a category with name {Name}", name);
        
        return Ok();
    }
    
    [HttpPost("category")]
    public IActionResult Create([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        _logger.LogInformation("Creating a category");
        
        return Ok();
    }
}