using AutoMapper;
using Ecommerce.API.Produts.DB;
using Ecommerce.API.Produts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Produts.Providers
{
    public class ProductsProvider: IProductsProvider
    {
        readonly ProductsDBContext _productsDBContext;
        readonly ILogger<ProductsProvider> _logger;
        readonly IMapper _mapper;
        public ProductsProvider(ProductsDBContext productsDBContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _productsDBContext = productsDBContext;
            _logger = logger;
            _mapper  = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_productsDBContext.Products.Any())
            {
                _productsDBContext.Products.Add(new Product { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                _productsDBContext.Products.Add(new Product { Id = 2, Name = "Mouse", Price = 5, Inventory = 500 });
                _productsDBContext.Products.Add(new Product { Id = 3, Name = "CPU", Price = 100, Inventory = 20 });
                _productsDBContext.Products.Add(new Product { Id = 4, Name = "Monitor", Price = 75, Inventory = 50 });
                _productsDBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _productsDBContext.Products.ToListAsync();
                if (products!= null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _productsDBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = _mapper.Map<DB.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
