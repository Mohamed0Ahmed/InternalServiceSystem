using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;
using System.Application.Services;

namespace MvcProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerPointsService _customerPointsService;
        private readonly IGuestService _guestService;

        public CustomerController(ICustomerService customerService, ICustomerPointsService customerPointsService, IGuestService guestService)
        {
            _customerService = customerService;
            _customerPointsService = customerPointsService;
            _guestService = guestService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string phoneNumber)
        {
            var guestId = HttpContext.Session.GetString("GuestId");
            if (string.IsNullOrEmpty(guestId))
            {
                return RedirectToAction("Login", "Guest");
            }

            var guest = await _guestService.GetGuestByIdAsync(guestId);
            var customer = await _customerService.GetOrCreateCustomerAsync(phoneNumber, guest.StoreId);
            HttpContext.Session.SetInt32("CustomerId", customer.Id);

            var points = await _customerPointsService.GetPointsAsync(customer.Id, guest.BranchId);
            ViewBag.Points = points.Points;

            return RedirectToAction("GetProducts", "Product");
        }
    }
}