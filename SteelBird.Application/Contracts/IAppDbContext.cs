using Microsoft.EntityFrameworkCore;
using SteelBird.Application.Wrappers;

namespace SteelBird.Application.Contracts;

public interface IAppDbContext : IDisposable, IAsyncDisposable
{
    Task<Result> TrySaveChangesAsync(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken ct = default);

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

}

