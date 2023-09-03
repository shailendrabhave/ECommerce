using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Produts.DB
{
    public class ProductsDBContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDBContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
