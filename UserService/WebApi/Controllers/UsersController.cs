using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _usersService.GetAll(cancellationToken); 
        
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create(CreateUserModel createUserModel, CancellationToken cancellationToken)
    {
        var user = NullGuard.ThrowIfNull(createUserModel).ToUser();

        await _usersService.Create(user, cancellationToken);

        return Ok(new { message = "Registration was successful" } );
    }
}