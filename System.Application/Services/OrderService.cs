using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerPointsService _customerPointsService;

        public OrderService(IUnitOfWork unitOfWork, ICustomerPointsService customerPointsService)
        {
            _unitOfWork = unitOfWork;
            _customerPointsService = customerPointsService;
        }

        public async Task<Order> CreateOrderAsync(int customerId, string guestId, int roomId, List<(int ProductId, int Quantity)> orderItems)
        {
            var order = new Order
            {
                CustomerId = customerId,
                GuestId = guestId,
                RoomId = roomId,
                OrderDate = DateTime.UtcNow,
                Status = Status.Pending,
                OrderItems = []
            };

            decimal totalPrice = 0;
            foreach (var item in orderItems)
            {
                var product = await _unitOfWork.Repository<Product, int>()
                    .GetAsync(p => p.Id == item.ProductId);

                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} not found.");
                }

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                order.OrderItems.Add(orderItem);
                totalPrice += product.Price * item.Quantity;
            }

            order.TotalPriceAtOrderTime = totalPrice;

            await _unitOfWork.Repository<Order, int>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
        {
            return await _unitOfWork.Repository<Order, int>()
                .FindAsync(o => o.Status == Status.Pending);
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Repository<Order, int>()
                .GetAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            order.Status = Status.Done;
            _unitOfWork.Repository<Order, int>().Update(order);

            var room = await _unitOfWork.Repository<Room, int>()
                .GetAsync(r => r.Id == order.RoomId);
            var pointsSetting = await _unitOfWork.Repository<PointsSetting, int>()
                .GetAsync(ps => ps.BranchId == room.BranchId);

            if (pointsSetting != null)
            {
                int points = (int)(order.TotalPriceAtOrderTime / pointsSetting.AmountPerPoint) * pointsSetting.PointsValue;
                await _customerPointsService.UpdatePointsAsync(order.CustomerId, room.BranchId, points);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Repository<Order, int>()
                .GetAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            order.Status = Status.Canceled;
            _unitOfWork.Repository<Order, int>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _unitOfWork.Repository<Order, int>()
                .FindAsync(o => o.CustomerId == customerId);
        }
    }
}