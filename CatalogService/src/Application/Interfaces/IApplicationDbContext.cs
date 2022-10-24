using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Category> Categories { get; }

    public DbSet<Item> Items { get; }
}