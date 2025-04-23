using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int customerId, string guestId, int roomId, List<(int ProductId, int Quantity)> orderItems);
        Task<IEnumerable<Order>> GetPendingOrdersAsync();
        Task ConfirmOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    }
}