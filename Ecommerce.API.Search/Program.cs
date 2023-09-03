using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpClient("OrderService", config => {
    config.BaseAddress = new Uri( builder.Configuration.GetValue<string>("Services:Orders"));
});
builder.Services.AddHttpClient("ProductsService", config => {
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Products"));
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddHttpClient("CustomersService", config => {
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Customers"));
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddControllers();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
