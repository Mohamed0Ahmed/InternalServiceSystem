using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Domain.Entities;
using System.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace MvcProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                ViewBag.Error = "You are not authorized to access this page.";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stores = await _context.Stores.Where(s => !s.IsDeleted && !s.IsHidden).ToListAsync();
            return View(stores);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateStore(string storeName)
        {
            var store = new Store
            {
                StoreName = storeName,
                IsDeleted = false,
                IsHidden = false
            };
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return Json(new { success = true, storeId = store.Id, storeName = store.StoreName });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditStore(int id, string storeName)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return Json(new { success = false, message = "Store not found." });

            store.StoreName = storeName;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return Json(new { success = false, message = "Store not found." });

            store.IsDeleted = true;
            store.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetBranches(int storeId)
        {
            var branches = await _context.Branches
                .Where(b => b.StoreId == storeId && !b.IsDeleted && !b.IsHidden)
                .ToListAsync();
            return Json(branches);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBranch(int storeId, string branchName)
        {
            var branch = new Branch
            {
                StoreId = storeId,
                BranchName = branchName,
                IsDeleted = false,
                IsHidden = false
            };
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            return Json(new { success = true, branchId = branch.Id, branchName = branch.BranchName });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditBranch(int id, string branchName)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return Json(new { success = false, message = "Branch not found." });

            branch.BranchName = branchName;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return Json(new { success = false, message = "Branch not found." });

            branch.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetRooms(int branchId)
        {
            var rooms = await _context.Rooms
                .Where(r => r.BranchId == branchId && !r.IsDeleted && !r.IsHidden)
                .ToListAsync();
            return Json(rooms);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRoom(int branchId, string roomName)
        {
            var room = new Room
            {
                BranchId = branchId,
                RoomName = roomName,
                IsDeleted = false,
                IsHidden = false
            };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return Json(new { success = true, roomId = room.Id, roomName = room.RoomName });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditRoom(int id, string roomName)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return Json(new { success = false, message = "Room not found." });

            room.RoomName = roomName;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return Json(new { success = false, message = "Room not found." });

            room.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetGuests(int roomId)
        {
            var guests = await _context.Guests
                .Where(g => g.RoomId == roomId && !g.IsDeleted && !g.IsHidden)
                .ToListAsync();
            return Json(guests);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGuest(int storeId, int branchId, int roomId, string username, string password, string phoneNumber)
        {
            var guest = new Guest
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Password = password,
                PhoneNumber = phoneNumber,
                StoreId = storeId,
                BranchId = branchId,
                RoomId = roomId,
                IsDeleted = false,
                IsHidden = false
            };
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
            return Json(new { success = true, guestId = guest.Id, username = guest.Username, phoneNumber = guest.PhoneNumber });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditGuest(string id, string username, string password, string phoneNumber)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return Json(new { success = false, message = "Guest not found." });

            guest.Username = username;
            guest.Password = password;
            guest.PhoneNumber = phoneNumber;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteGuest(string id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return Json(new { success = false, message = "Guest not found." });

            guest.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await _userManager.GetUsersInRoleAsync("Owner");
            return Json(owners.Select(o => new { Id = o.Id, Username = o.UserName, Email = o.Email }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateOwner(string username, string password, string email)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Owner");
                return Json(new { success = true, ownerId = user.Id, username = user.UserName, email = user.Email });
            }
            return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditOwner(string id, string username, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return Json(new { success = false, message = "Owner not found." });

            user.UserName = username;
            user.Email = email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteOwner(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return Json(new { success = false, message = "Owner not found." });

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }
    }
}