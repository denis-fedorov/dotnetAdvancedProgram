using System.Reflection;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class RepositoryDbContext : DbContext, IRepositoryDbContext
{
    public DbSet<UserDto> Users => Set<UserDto>();
    
    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        // do nothing
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await base.SaveChangesAsync(ct);
    }
}