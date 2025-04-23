using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Application.Services;

namespace MvcProject.Controllers
{
    public class HelpRequestController : Controller
    {
        private readonly IHelpRequestService _helpRequestService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IGuestService _guestService;

        public HelpRequestController(IHelpRequestService helpRequestService, IHubContext<NotificationHub> hubContext, IGuestService guestService)
        {
            _helpRequestService = helpRequestService;
            _hubContext = hubContext;
            _guestService = guestService;
        }

        [Authorize(Roles = "Guest,Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateHelpRequest(string requestType, string details)
        {
            var guestId = HttpContext.Session.GetString("GuestId");
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            if (string.IsNullOrEmpty(guestId) || !customerId.HasValue)
            {
                return RedirectToAction("Login", "Guest");
            }

            var guest = await _guestService.GetGuestByIdAsync(guestId);
            var helpRequest = await _helpRequestService.CreateHelpRequestAsync(customerId.Value, guestId, guest.RoomId, requestType, details);

            await _hubContext.Clients.Group("Owners").SendAsync("ReceiveHelpRequestNotification", helpRequest.Id);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> PendingHelpRequests()
        {
            var helpRequests = await _helpRequestService.GetPendingHelpRequestsAsync();
            return View(helpRequests);
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> ConfirmHelpRequest(int id)
        {
            await _helpRequestService.ConfirmHelpRequestAsync(id);
            return RedirectToAction("PendingHelpRequests");
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CancelHelpRequest(int id)
        {
            await _helpRequestService.CancelHelpRequestAsync(id);
            return RedirectToAction("PendingHelpRequests");
        }
    }
}