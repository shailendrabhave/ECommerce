using Ecommerce.API.Search.Models;

namespace Ecommerce.API.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
