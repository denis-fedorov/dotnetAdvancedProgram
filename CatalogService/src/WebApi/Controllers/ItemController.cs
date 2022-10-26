using Application.Requests.Items.GetItem;
using Application.Requests.Items.GetItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/item")]
public class ItemController : SenderControllerBase
{
    private readonly ILogger<ItemController> _logger;

    public ItemController(ISender sender, ILogger<ItemController> logger)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Get all the items");
        
        var request = new GetItemsQuery();
        var result = await Sender.Send(request);

        return Ok(result);
    }

    [HttpGet("name/{name}")]
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
}