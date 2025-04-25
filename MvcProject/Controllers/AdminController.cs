using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;


namespace MvcProject.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _adminService.GetAllStoresAsync();
            return View(stores);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var isAdmin = await _adminService.AdminLoginAsync(username, password);
            if (isAdmin)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await _adminService.GetAllOwnersAsync();
            return Json(owners);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner(string username, string password, string email, int storeId)
        {
            var success = await _adminService.CreateOwnerAsync(username, password, email, storeId);
            if (success)
            {
                var owners = await _adminService.GetAllOwnersAsync();
                var owner = owners.FirstOrDefault(u => u.UserName == username);
                return Json(new { success = true, ownerId = owner?.Id, username, email });
            }
            return Json(new { success = false, message = "Failed to create owner." });
        }

        [HttpPost]
        public async Task<IActionResult> EditOwner(string id, string username, string email)
        {
            var success = await _adminService.UpdateOwnerAsync(id, username, email);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOwner(string id)
        {
            var success = await _adminService.DeleteOwnerAsync(id);
            return Json(new { success });
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedStores()
        {
            var deletedStores = await _adminService.GetDeletedStoresAsync();
            return Json(deletedStores);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(string storeName)
        {
            var success = await _adminService.CreateStoreAsync(storeName);
            if (success)
            {
                var stores = await _adminService.GetAllStoresAsync();
                var store = stores.FirstOrDefault(s => s.StoreName == storeName);
                return Json(new { success = true, storeId = store?.Id, storeName });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(int id, string storeName)
        {
            var success = await _adminService.UpdateStoreAsync(id, storeName);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var success = await _adminService.DeleteStoreAsync(id);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> RestoreStore(int id)
        {
            var success = await _adminService.RestoreStoreAsync(id);
            if (success)
            {
                var stores = await _adminService.GetAllStoresAsync();
                var store = stores.FirstOrDefault(s => s.Id == id);
                return Json(new { success = true, storeId = store?.Id, storeName = store?.StoreName });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> GetBranches(int storeId)
        {
            var branches = await _adminService.GetBranchesByStoreIdAsync(storeId);
            return Json(branches);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(int storeId, string branchName)
        {
            var success = await _adminService.CreateBranchAsync(storeId, branchName);
            if (success)
            {
                var branches = await _adminService.GetBranchesByStoreIdAsync(storeId);
                var branch = branches.FirstOrDefault(b => b.BranchName == branchName);
                return Json(new { success = true, branchId = branch?.Id, branchName });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> EditBranch(int id, string branchName)
        {
            var success = await _adminService.UpdateBranchAsync(id, branchName);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var success = await _adminService.DeleteBranchAsync(id);
            return Json(new { success });
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms(int branchId)
        {
            var rooms = await _adminService.GetRoomsByBranchIdAsync(branchId);
            return Json(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(int branchId, string roomName)
        {
            var success = await _adminService.CreateRoomAsync(branchId, roomName);
            if (success)
            {
                var rooms = await _adminService.GetRoomsByBranchIdAsync(branchId);
                var room = rooms.FirstOrDefault(r => r.RoomName == roomName);
                return Json(new { success = true, roomId = room?.Id, roomName });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(int id, string roomName)
        {
            var success = await _adminService.UpdateRoomAsync(id, roomName);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var success = await _adminService.DeleteRoomAsync(id);
            return Json(new { success });
        }

        [HttpGet]
        public async Task<IActionResult> GetGuests(int roomId)
        {
            var guests = await _adminService.GetGuestsByRoomIdAsync(roomId);
            return Json(guests);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest(int storeId, int branchId, int roomId, string username, string password, string phoneNumber)
        {
            var success = await _adminService.CreateGuestAsync(storeId, branchId, roomId, username, password, phoneNumber);
            if (success)
            {
                var guests = await _adminService.GetGuestsByRoomIdAsync(roomId);
                var guest = guests.FirstOrDefault(g => g.Username == username);
                return Json(new { success = true, guestId = guest?.Id, username, phoneNumber, password });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> EditGuest(string id, string username, string password, string phoneNumber)
        {
            var success = await _adminService.UpdateGuestAsync(id, username, password, phoneNumber);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGuest(string id)
        {
            var success = await _adminService.DeleteGuestAsync(id);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _adminService.LogoutAsync();
            return RedirectToAction("Login", "Account", new { area = "" });
        }
    }
}
