namespace Ecommerce.API.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int customerId);
    }
}
