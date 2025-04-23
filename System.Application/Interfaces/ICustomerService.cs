using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetOrCreateCustomerAsync(string phoneNumber, int storeId);
        Task<Customer> GetCustomerByIdAsync(int customerId);
    }
}