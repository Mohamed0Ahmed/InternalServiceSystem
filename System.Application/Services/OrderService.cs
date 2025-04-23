using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomService _roomService;

        public OrderService(IUnitOfWork unitOfWork, IRoomService roomService)
        {
            _unitOfWork = unitOfWork;
            _roomService = roomService;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Order,int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Order, int>().GetAllAsync();
        }

        public async Task<IEnumerable<Order>> GetByGuestIdAsync(string guestId)
        {
            return await _unitOfWork.Repository<Order, int>().FindAsync(o => o.PhoneNumber == guestId);
        }

        public async Task AddAsync(Order order, IEnumerable<OrderItem> orderItems)
        {
            var guest = await _unitOfWork.Repository<Guest, string>().GetByIdAsync(order.PhoneNumber);
            if (guest == null)
            {
                throw new InvalidOperationException($"Guest with ID {order.PhoneNumber} not found.");
            }

            if (!await _roomService.CheckAvailabilityAsync(order.RoomId))
            {
                throw new InvalidOperationException($"Room with ID {order.RoomId} is not available.");
            }

            //order.totalPrice = await CalculateTotalPriceAsync(orderItems);



            await _unitOfWork.Repository<Order, int>().AddAsync(order);
            foreach (var item in orderItems)
            {
                item.OrderId = order.Id;
                await _unitOfWork.Repository<OrderItem, int>().AddAsync(item);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            var guest = await _unitOfWork.Repository<Guest,string>().GetByIdAsync(order.PhoneNumber);
            if (guest == null)
            {
                throw new InvalidOperationException($"Guest with ID {order.PhoneNumber} not found.");
            }

            _unitOfWork.Repository<Order, int>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _unitOfWork.Repository<Order, int>().GetByIdAsync(id);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with ID {id} not found.");
            }

            _unitOfWork.Repository<Order, int>().Delete(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderItem> orderItems)
        {
            return await Task.FromResult(orderItems.Sum(item => item.Quantity * item.UnitPrice));
        }
    }
}