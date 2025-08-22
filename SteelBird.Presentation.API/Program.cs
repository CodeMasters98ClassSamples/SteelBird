using Microsoft.EntityFrameworkCore;
using SteelBird.Infrastructure;
using SteelBird.Application;
using SteelBird.Presentation.API.Extensions;
using SteelBird.Infrastructure.Identity;
using Serilog;
using SteelBird.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using SteelBird.Infrastructure.Identity.Models;
using SteelBird.Domain.Enums;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
if (string.IsNullOrEmpty(environment))
{
    //Log
}
var connectionString =
    builder.Configuration.GetConnectionString("CoreDatabaseContext")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .RegisterPresentationLayer()
    .RegisterApplicationLayer()
    .RegisterInfrastructureIdentityServices(builder.Configuration, useInMemoryDatabase)
    .RegisterInfrastructureLayer(connectionString: connectionString, useInMemoryDatabase)
    .AddVersioning()
    .AddMySwagger();

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
        //Role Managnet
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await roleManager.CreateAsync(new IdentityRole(UserRoles.USER.ToString()));
        await roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN.ToString()));


        //UserManagment
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

        //Product Manage
        var context = scope.ServiceProvider.GetRequiredService<CoreDatabaseContext>();
        //context.Products.AddRange(
        //    new Product { Id = 1, Name = "Notebook",Barcode = "1", Description = "1", Price = new Money(amount: 1000, currency: "RIAL") },
        //    new Product { Id = 2, Name = "Pen", Barcode = "2",Description = "2", Price = new Money(amount: 1000, currency: "RIAL") }
        //);
        //context.SaveChanges();
    }
}

app.Run();
