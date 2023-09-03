using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Models;
using System.Text.Json;

namespace Ecommerce.API.Search.Services
{
    public class OrderService:IOrderService
    {
        private readonly IHttpClientFactory _clientFactory;   
        private readonly ILogger<OrderService> _logger;
        public OrderService(IHttpClientFactory clientFactory, ILogger<OrderService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;

        }

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = _clientFactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
