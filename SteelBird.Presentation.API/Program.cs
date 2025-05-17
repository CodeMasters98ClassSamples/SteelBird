using SteelBird.Presentation.API.Contracts;
using SteelBird.Presentation.API.Entities;
using SteelBird.Presentation.API.Service;
using SteelBird.Presentation.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IBaseService<Product>, ProductService>();
//builder.Services.AddScoped<IBaseService<Product>, MockProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
