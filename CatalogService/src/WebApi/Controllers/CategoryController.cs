using Application.Requests.Categories.CreateCategory;
using Application.Requests.Categories.GetCategories;
using Application.Requests.Categories.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        var result = (await _sender.Send(request)).ToList();

        return Ok(result);
    }

    [HttpGet("category/name/{name}")]
    public async Task<IActionResult> Get(string name)
    {
        _logger.LogInformation("Getting a category with name {Name}", name);

        var request = new GetCategoryQuery(name);
        var result = await _sender.Send(request);

        if (result is null)
        {
            return NotFound($"Category '{name}' was not found");
        }
        
        return Ok(result);
    }
    
    [HttpPost("category")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryModel createCategoryModel)
    {
        _logger.LogInformation("Creating a category");

        var request = new CreateCategoryCommand(createCategoryModel);
        await _sender.Send(request);

        return Created(createCategoryModel.Name, createCategoryModel.Name);
    }
}