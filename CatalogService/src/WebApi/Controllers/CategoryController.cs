using Application.Requests.Categories.CreateCategory;
using Application.Requests.Categories.GetCategories;
using Application.Requests.Categories.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/category")]
public class CategoryController : SenderControllerBase
{
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger, ISender sender)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all categories");

        var request = new GetCategoriesQuery();
        var result = (await Sender.Send(request)).ToList();

        return Ok(result);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> Get(string name)
    {
        _logger.LogInformation("Getting a category with name {Name}", name);

        var request = new GetCategoryQuery(name);
        var result = await Sender.Send(request);

        if (result is null)
        {
            return NotFound($"Category '{name}' was not found");
        }
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryModel createCategoryModel)
    {
        _logger.LogInformation("Creating a category");

        var request = new CreateCategoryCommand(createCategoryModel);
        await Sender.Send(request);

        return Created(createCategoryModel.Name, createCategoryModel.Name);
    }
}