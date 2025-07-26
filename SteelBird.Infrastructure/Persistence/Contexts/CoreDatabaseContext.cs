using Microsoft.EntityFrameworkCore;
using SteelBird.Application.Contracts;
using SteelBird.Application.Wrappers;
using SteelBird.Domain.Entities;
using System.Threading;

namespace SteelBird.Infrastructure.Persistence.Contexts;

public class CoreDatabaseContext : DbContext, IAppDbContext
{
    public CoreDatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public async Task<Result> TrySaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    async Task IAppDbContext.SaveChangesAsync(CancellationToken ct)
    {
        await base.SaveChangesAsync(ct);
    }
}
