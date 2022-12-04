using Application.Requests.Items.CreateItem;
using Application.Requests.Items.DeleteItem;
using Application.Requests.Items.GetItem;
using Application.Requests.Items.GetItems;
using Application.Requests.Items.UpdateItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : SenderControllerBase
{
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(ISender sender, ILogger<ItemsController> logger)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetItemsModel getItemsModel)
    {
        _logger.LogInformation("Get all the items with {@GetItemsModel} param", getItemsModel);
        if (!getItemsModel.IsValid())
        {
            return BadRequest();
        }
        
        var request = new GetItemsQuery(getItemsModel);
        var result = await Sender.Send(request);

        return Ok(result);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> Get(string name)
    {
        _logger.LogInformation("Get an item '{Name}'", name);

        var request = new GetItemQuery(name);
        var result = await Sender.Send(request);

        if (result is null)
        {
            return NotFound(name);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemModel createItemModel)
    {
        _logger.LogInformation("Create an item: {@CreateItemModel}", createItemModel);

        var command = new CreateItemCommand(createItemModel);
        await Sender.Send(command);
        
        return CreatedAtAction(nameof(Create), new { createItemModel.Name });
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, [FromBody] UpdateItemModel updateItemModel)
    {
        _logger.LogInformation("Update an item '{Name}'", name);

        var command = new UpdateItemCommand(name, updateItemModel);
        await Sender.Send(command);

        return NoContent();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        _logger.LogInformation("Deleting an item with name {Name}", name);
        
        var request = new DeleteItemCommand(name);
        await Sender.Send(request);

        return NoContent();
    }
}