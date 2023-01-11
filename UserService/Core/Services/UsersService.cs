using Core.Entities;
using Core.Interfaces;
using SharedKernel;

namespace Core.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ITokenService _tokenService;

    public UsersService(IUsersRepository usersRepository, ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _tokenService = tokenService;
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _usersRepository.GetAll(cancellationToken);
    }

    public async Task Create(User user, CancellationToken cancellationToken)
    {
        NullGuard.ThrowIfNull(user);
        
        await _usersRepository.Create(user, cancellationToken);
    }

    public async Task<string?> Login(
        string username, string password, string host, CancellationToken cancellationToken)
    {
        NullGuard.ThrowIfNull(username);
        NullGuard.ThrowIfNull(password);
        NullGuard.ThrowIfNull(host);
        
        var user = await _usersRepository.Get(username, password, cancellationToken);
        
        return user is not null
            ? _tokenService.GenerateToken(user, host)
            : null;
    }
}