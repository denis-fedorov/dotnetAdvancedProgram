using Application.Categories.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ISender _sender;

    public CategoryController(ILogger<CategoryController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all categories");

        var request = new GetCategoriesQuery();
        var result = await _sender.Send(request);

        return Ok(result);
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