using Core.Entities;

namespace Core.Interfaces;

public interface IUsersRepository
{
    public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);
    
    public Task<User?> Get(string username, string password, CancellationToken cancellationToken);

    public Task Create(User user, CancellationToken cancellationToken);
}