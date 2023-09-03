using Ecommerce.API.Produts.DB;
using Ecommerce.API.Produts.Interfaces;
using Ecommerce.API.Produts.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProductsProvider, ProductsProvider>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ProductsDBContext>(options =>
{
    options.UseInMemoryDatabase("Products");
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
