using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteelBird.Presentation.API.Contracts;
using SteelBird.Presentation.API.Database;
using SteelBird.Presentation.API.Entities;
using SteelBird.Presentation.API.Profiles;
using SteelBird.Presentation.API.Service;
using SteelBird.Presentation.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IBaseService<Product>, ProductService>();
//builder.Services.AddScoped<IBaseService<Product>, MockProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


builder.Services.AddFluentValidation();

builder.Services.AddHealthChecks();


builder.Services.AddAutoMapper(typeof(ProductProfile));

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

builder.Services.AddDbContext<CoreDatabaseContext>(options =>
    options.UseSqlServer(connectionString));


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

app.UseAuthentication();

app.UseAuthorization();

app.Run();
