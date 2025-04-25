using Microsoft.AspNetCore.Identity;
using System.Infrastructure.Abstraction;
using System.Infrastructure.Persistence.Identity;
using System.Infrastructure.Persistence;

namespace System.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllOwnersAsync()
        {
            var owners = await _userManager.GetUsersInRoleAsync("Owner");
            return owners
                .Where(u => u.StoreId != null)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToList();
        }

        public async Task<bool> CreateOwnerAsync(string username, string password, string email, int storeId)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
                return false;

            var store = await _context.Stores.FindAsync(storeId);
            if (store == null || store.IsDeleted || store.IsHidden)
                return false;

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                StoreId = storeId
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return false;

            await _userManager.AddToRoleAsync(user, "Owner");
            return true;
        }

        public async Task<bool> UpdateOwnerAsync(string id, string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
                return false;

            var user = await _userManager.FindByIdAsync(id);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Owner"))
                return false;

            user.UserName = username;
            user.Email = email;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteOwnerAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Owner"))
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<int?> GetOwnerStoreIdAsync(string ownerId)
        {
            var user = await _userManager.FindByIdAsync(ownerId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Owner"))
                return null;

            return user.StoreId;
        }

        public async Task<bool> AdminLoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded && (await _userManager.IsInRoleAsync(user, "Admin"));
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

     
    }
}