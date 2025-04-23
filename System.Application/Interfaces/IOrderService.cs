using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetByGuestIdAsync(string guestId);
        Task AddAsync(Order order, IEnumerable<OrderItem> orderItems);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderItem> orderItems);
    }
}