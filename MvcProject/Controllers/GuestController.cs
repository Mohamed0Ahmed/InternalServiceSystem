using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;

namespace MvcProject.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string storeName)
        {
            try
            {
                var guest = await _guestService.AuthenticateAsync(username, password, storeName);
                HttpContext.Session.SetString("GuestId", guest.Id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}