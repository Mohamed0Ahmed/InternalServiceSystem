namespace System.Infrastructure.Abstraction
{
    public interface IIdentityService
    {
        Task<IEnumerable<UserDto>> GetAllOwnersAsync();
        Task<bool> CreateOwnerAsync(string username, string password, string email, int storeId);
        Task<bool> UpdateOwnerAsync(string id, string username, string email);
        Task<bool> DeleteOwnerAsync(string id);
        Task<int?> GetOwnerStoreIdAsync(string ownerId);
        Task<bool> AdminLoginAsync(string username, string password);
        Task LogoutAsync();
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}