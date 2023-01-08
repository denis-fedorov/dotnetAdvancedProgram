using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _usersRepository.GetAll(cancellationToken);
    }

    public async Task Create(User user, CancellationToken cancellationToken)
    {
        await _usersRepository.Create(user, cancellationToken);
    }
}