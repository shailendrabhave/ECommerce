using AutoMapper;
using ECommerce.API.Customers.DB;
using ECommerce.API.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.Providers
{
    public class CustomerProvider: ICustomerProvider
    {
        readonly CustomerDBContext _customerDBContext;
        readonly ILogger<CustomerProvider> _logger;
        readonly IMapper _mapper;
        public CustomerProvider(CustomerDBContext customerDBContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            _customerDBContext = customerDBContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_customerDBContext.Customers.Any())
            {
                _customerDBContext.Customers.Add(new Customer { Id = 1, Name = "John Doe", Address = "USA" });
                _customerDBContext.Customers.Add(new Customer { Id = 2, Name = "Shailendra Bhave", Address = "India" });
                _customerDBContext.Customers.Add(new Customer { Id = 3, Name = "Customer 2", Address = "United Kingdom" });
                _customerDBContext.Customers.Add(new Customer { Id = 4, Name = "Customer 3", Address = "Netherlands" });
                _customerDBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _customerDBContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(customers);
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

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _customerDBContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<DB.Customer, Models.Customer>(customer);
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
