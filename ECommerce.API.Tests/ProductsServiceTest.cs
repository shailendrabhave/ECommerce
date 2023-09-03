using AutoMapper;
using Ecommerce.API.Produts.DB;
using Ecommerce.API.Produts.Profiles;
using Ecommerce.API.Produts.Providers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDBContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductsDBContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDBContext>().UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidId)).Options;
            var dbContext = new ProductsDBContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDBContext>().UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInvalidId)).Options;
            var dbContext = new ProductsDBContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }
        private void CreateProducts(ProductsDBContext dbContext)
        {
            for(int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                }) ;
            }
            dbContext.SaveChanges();
        }
    }
}