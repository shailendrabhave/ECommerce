using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.DB
{
    public class CustomerDBContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
