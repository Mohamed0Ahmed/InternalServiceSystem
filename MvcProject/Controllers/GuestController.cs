using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;
using QRCoder;

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
                return RedirectToAction( "Login" , "Customer");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult GenerateQRCode(string guestId)
        {
            if (string.IsNullOrEmpty(guestId))
            {
                return BadRequest("Guest ID is required.");
            }

            //var url = Url.Action("Login", "Customer", new { guestId }, Request.Scheme);
            //using var qrGenerator = new QRCodeGenerator();
            //var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            //using var qrCode = new QRCode(qrCodeData);
            //using var qrCodeImage = qrCode.GetGraphic(20);
            //using var ms = new MemoryStream();
            //qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //var qrCodeBytes = ms.ToArray();
            //var base64String = Convert.ToBase64String(qrCodeBytes);
            return Json(new { qrCodeImage = $"data:image/png;base64," });
        }
    }
}