using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces;

public interface IRepositoryDbContext
{
    public DbSet<UserDto> Users { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct);
}