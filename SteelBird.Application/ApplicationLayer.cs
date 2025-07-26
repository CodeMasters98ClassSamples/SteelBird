using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using SteelBird.Presentation.API.Profiles;

namespace SteelBird.Application;

public static class ApplicationLayer
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationLayer).Assembly));

        services.AddFluentValidation();

        services.AddAutoMapper(typeof(ProductProfile));

        return services;
    }
}
