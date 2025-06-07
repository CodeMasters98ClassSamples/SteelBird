using Microsoft.EntityFrameworkCore;
using SteelBird.Domain.Entities;

namespace SteelBird.Infrastructure.Persistence.Contexts;

public class CoreDatabaseContext : DbContext
{
    public CoreDatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
