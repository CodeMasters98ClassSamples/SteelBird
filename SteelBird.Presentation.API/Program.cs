using Microsoft.EntityFrameworkCore;
using SteelBird.Infrastructure;
using SteelBird.Application;
using SteelBird.Presentation.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("CoreDatabaseContext")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services
    .RegisterPresentationLayer()
    .RegisterApplicationLayer()
    .RegisterInfrastructureLayer(connectionString: connectionString)
    .AddVersioning();

var app = builder.Build();

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
