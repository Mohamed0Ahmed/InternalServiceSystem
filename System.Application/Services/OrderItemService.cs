using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _unitOfWork.Repository<OrderItem,int>().FindAsync(oi => oi.OrderId == orderId);
        }

        public async Task AddRangeAsync(IEnumerable<OrderItem> orderItems)
        {
            foreach (var item in orderItems)
            {
                var product = await _unitOfWork.Repository<Product,int>().GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");
                }

                await _unitOfWork.Repository<OrderItem,int>().AddAsync(item);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await _unitOfWork.Repository<OrderItem, int>().GetByIdAsync(id);
            if (orderItem == null)
            {
                throw new InvalidOperationException($"OrderItem with ID {id} not found.");
            }

            _unitOfWork.Repository<OrderItem, int>().Delete(orderItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}