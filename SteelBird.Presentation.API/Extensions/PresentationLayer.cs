using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace SteelBird.Presentation.API.Extensions;

public static class PresentationLayer
{
    public static IServiceCollection RegisterPresentationLayer(this IServiceCollection services)
    {
        services.AddHealthChecks();

        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));
        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0); // Default to v1.0
            options.AssumeDefaultVersionWhenUnspecified = true; // Assume default if not specified
            options.ReportApiVersions = true; // Return headers "api-supported-versions" and "api-deprecated-versions"
            options.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"), // ?api-version=1.0
            new HeaderApiVersionReader("X-Version"), // Custom header
            new MediaTypeApiVersionReader("ver") // Accept: application/json;ver=1.0
            );
        });

        return services;
    }

}
