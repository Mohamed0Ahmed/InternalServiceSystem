using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Application.Services;

namespace MvcProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IGuestService _guestService;

        public OrderController(IOrderService orderService, IHubContext<NotificationHub> hubContext, IGuestService guestService)
        {
            _orderService = orderService;
            _hubContext = hubContext;
           _guestService = guestService;
        }

        [Authorize(Roles = "Guest,Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(List<(int ProductId, int Quantity)> orderItems)
        {
            var guestId = HttpContext.Session.GetString("GuestId");
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            if (string.IsNullOrEmpty(guestId) || !customerId.HasValue)
            {
                return RedirectToAction("Login", "Guest");
            }

            var guest = await _guestService.GetGuestByIdAsync(guestId);
            var order = await _orderService.CreateOrderAsync(customerId.Value, guestId, guest.RoomId, orderItems);

            await _hubContext.Clients.Group("Owners").SendAsync("ReceiveOrderNotification", order.Id);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> PendingOrders()
        {
            var orders = await _orderService.GetPendingOrdersAsync();
            return View(orders);
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            await _orderService.ConfirmOrderAsync(id);
            return RedirectToAction("PendingOrders");
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await _orderService.CancelOrderAsync(id);
            return RedirectToAction("PendingOrders");
        }
    }
}