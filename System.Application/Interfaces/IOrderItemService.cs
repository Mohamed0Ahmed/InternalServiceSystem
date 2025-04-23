using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
        Task AddRangeAsync(IEnumerable<OrderItem> orderItems);
        Task DeleteAsync(int id);
    }
}