using ECommerce.API.Customers.DB;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICustomerProvider, CustomerProvider>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<CustomerDBContext>(options => {
    options.UseInMemoryDatabase("Customers");
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
