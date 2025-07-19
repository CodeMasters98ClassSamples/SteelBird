using Microsoft.EntityFrameworkCore;
using SteelBird.Infrastructure;
using SteelBird.Application;
using SteelBird.Presentation.API.Extensions;
using SteelBird.Infrastructure.Identity;
using Serilog;
using SteelBird.Infrastructure.Persistence.Contexts;
using SteelBird.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using SteelBird.Infrastructure.Identity.Models;
using Azure.Core;
using Amazon.Runtime.Internal;
using SteelBird.Domain.Enums;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your token.\nExample: Bearer eyJhbGciOiJIUzI1..."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var connectionString =
    builder.Configuration.GetConnectionString("CoreDatabaseContext")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services
    .RegisterPresentationLayer()
    .RegisterApplicationLayer()
    .RegisterInfrastructureIdentityServices(builder.Configuration, useInMemoryDatabase)
    .RegisterInfrastructureLayer(connectionString: connectionString, useInMemoryDatabase)
    .AddVersioning();

if (builder.Environment.IsProduction())
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
}
builder.Host.UseSerilog();

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

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await roleManager.CreateAsync(new IdentityRole(UserRoles.USER.ToString()));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN.ToString()));

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = new ApplicationUser
        {
            Email = "darvishiparham14@gmail.com",
            FirstName = "parham",
            LastName = "darvishi",
            UserName = "darvishiparham14@gmail.com"
        };
        var result = await userManager.CreateAsync(user, "Pp@1234");
        await userManager.AddToRoleAsync(user, UserRoles.ADMIN.ToString());

        var context = scope.ServiceProvider.GetRequiredService<CoreDatabaseContext>();
        context.Products.AddRange(
            new Product { Id = 1, Name = "Notebook", Price = 5.99m,Barcode = "1", Description = "1" },
            new Product { Id = 2, Name = "Pen", Price = 1.49m, Barcode = "2",Description = "2" }
        );
        context.SaveChanges();
    }
}


app.Run();
