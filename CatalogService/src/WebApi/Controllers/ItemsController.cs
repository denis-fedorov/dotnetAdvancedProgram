﻿using Application.Requests.Items.CreateItem;
using Application.Requests.Items.GetItem;
using Application.Requests.Items.GetItems;
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
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Get all the items");
        
        var request = new GetItemsQuery();
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
}