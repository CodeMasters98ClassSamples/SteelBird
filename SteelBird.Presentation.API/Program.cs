
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteelBird.Infrastructure;
using SteelBird.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddHealthChecks();

builder.Services.AddApiVersioning(options =>
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

var connectionString =
    builder.Configuration.GetConnectionString("CoreDatabaseContext")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services
    .RegisterApplicationLayer()
    .RegisterInfrastructureLayer(connectionString: connectionString);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseCors();

app.MapHealthChecks("/healthz");

app.UseAuthentication();

app.UseAuthorization();

app.Run();
