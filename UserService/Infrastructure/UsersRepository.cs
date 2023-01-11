using Core.Entities;
using Core.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure;

public class UsersRepository : IUsersRepository
{
    private readonly IRepositoryDbContext _repositoryDbContext;

    public UsersRepository(IRepositoryDbContext repositoryDbContext)
    {
        _repositoryDbContext = repositoryDbContext;
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _repositoryDbContext.Users
            .Select(u => u.ToEntity())
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> Get(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _repositoryDbContext.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password, cancellationToken);

        return user?.ToEntity();
    }

    public async Task Create(User user, CancellationToken cancellationToken)
    {
        NullGuard.ThrowIfNull(user);
        
        var dto = user.ToDto();

        await _repositoryDbContext.Users.AddAsync(dto, cancellationToken);
        await _repositoryDbContext.SaveChangesAsync(cancellationToken);
    }
}