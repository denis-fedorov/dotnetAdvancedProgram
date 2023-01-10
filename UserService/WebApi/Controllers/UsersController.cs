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
    private readonly ITokenService _tokenService;

    public UsersController(IUsersService usersService, ITokenService tokenService)
    {
        _usersService = usersService;
        _tokenService = tokenService;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel loginModel, CancellationToken cancellationToken)
    {
        NullGuard.ThrowIfNull(loginModel);
        var username = NullGuard.ThrowIfNull(loginModel.Username);
        var password = NullGuard.ThrowIfNull(loginModel.Password);
        
        var token = await _usersService.Login(username, password, cancellationToken);
        if (token is null)
        {
            return Unauthorized();
        }
        
        Response.Headers["Authorization"] = token;
        
        return Ok(new { Token = token } );
    }

    [HttpPost("validate")]
    public IActionResult ValidateToken(ValidateTokenModel validateTokenModel)
    {
        NullGuard.ThrowIfNull(validateTokenModel);

        var token = validateTokenModel.Token;
        var isTokenValid = _tokenService.IsTokenValid(token);
        
        return isTokenValid
            ? Ok()
            : Unauthorized();
    }
}