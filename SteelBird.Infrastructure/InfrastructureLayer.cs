using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SteelBird.Domain.Entities;
using SteelBird.Infrastructure.Persistence.Contexts;
using SteelBird.Presentation.API.Contracts;
using SteelBird.Presentation.API.Service;

namespace SteelBird.Infrastructure;

public static class InfrastructureLayer
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services,string connectionString)
    {
        services.AddDbContext<CoreDatabaseContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IBaseService<Product>, ProductService>();
        return services;
    }
}
