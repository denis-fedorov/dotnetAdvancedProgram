using Application.Requests.Categories.CreateCategory;
using Application.Requests.Categories.DeleteCategory;
using Application.Requests.Categories.GetCategories;
using Application.Requests.Categories.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Settings.Model;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : SenderControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly CategoryResourceFactory _categoryResourceFactory;

    public CategoriesController(
        ILogger<CategoriesController> logger,
        ISender sender,
        CategoryResourceFactory categoryResourceFactory)
            : base(sender)
    {
        _logger = logger;
        _categoryResourceFactory = categoryResourceFactory;
    }

    [HttpGet(Name = nameof(GetAll))]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all categories");

        var request = new GetCategoriesQuery();
        var result = await Sender.Send(request);

        return Ok(_categoryResourceFactory.CreateCategoriesResourceList(result.Categories));
    }

    [HttpGet("{name}", Name = nameof(Get))]
    public async Task<IActionResult> Get(string name)
    {
        _logger.LogInformation("Getting a category with name {Name}", name);

        var request = new GetCategoryQuery(name);
        var result = await Sender.Send(request);

        if (result is null)
        {
            return NotFound($"Category '{name}' was not found");
        }
        
        return Ok(_categoryResourceFactory.CreateCategoryResource(result));
    }
    
    [HttpPost(Name = nameof(Create))]
    public async Task<IActionResult> Create([FromBody] CreateCategoryModel createCategoryModel)
    {
        _logger.LogInformation("Creating a category: {@CreateCategoryModel}", createCategoryModel);

        var request = new CreateCategoryCommand(createCategoryModel);
        await Sender.Send(request);

        return CreatedAtAction(nameof(Create), new { createCategoryModel.Name });
    }

    [HttpDelete("{name}", Name = nameof(Delete))]
    public async Task<IActionResult> Delete(string name)
    {
        _logger.LogInformation("Deleting a category with name {Name}", name);

        var request = new DeleteCategoryCommand(name);
        await Sender.Send(request);

        return NoContent();
    }
}