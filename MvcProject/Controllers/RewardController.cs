using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;
using System.Application.Services;

namespace MvcProject.Controllers
{
    public class RewardController : Controller
    {
        private readonly IRewardService _rewardService;
        private readonly IGuestService _guestService;
        private readonly ICustomerPointsService _customerPointsService;

        public RewardController(IRewardService rewardService, ICustomerPointsService customerPointsService , IGuestService guestService)
        {
            _rewardService = rewardService;
            _guestService = guestService;
            _customerPointsService = customerPointsService;
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> CreateReward(int branchId, string name, int requiredPoints)
        {
            await _rewardService.CreateRewardAsync(branchId, name, requiredPoints);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RedeemReward(int rewardId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            var guestId = HttpContext.Session.GetString("GuestId");

            if (!customerId.HasValue || string.IsNullOrEmpty(guestId))
            {
                return RedirectToAction("Login", "Guest");
            }

            var guest = await _guestService.GetGuestByIdAsync(guestId);
            await _customerPointsService.RedeemPointsAsync(customerId.Value, guest.BranchId, rewardId);

            return RedirectToAction("Index", "Home");
        }
    }
}