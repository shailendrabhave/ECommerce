using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Models;

namespace Ecommerce.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductsService _productsService;
        private readonly ICustomerService _customerService;
        public SearchService(IOrderService orderService, IProductsService productsService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productsService = productsService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await _orderService.GetOrdersAsync(customerId);
            var productResult = await _productsService.GetProductsAsync();
            var customerResult = await _customerService.GetCustomerAsync(customerId);
            if (orderResult.IsSuccess)
            {
                foreach(var order in orderResult.Orders) { 
                    foreach(var orderItem in order.Items) {
                        orderItem.ProductName = productResult.IsSuccess ? 
                                productResult.Products.FirstOrDefault(p => p.Id == orderItem.ProductId)?.Name : 
                                "Product information is not available";

                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess ?
                                customerResult.Customer:
                                "Customer information is not available",
                    Orders = orderResult.Orders,

                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
