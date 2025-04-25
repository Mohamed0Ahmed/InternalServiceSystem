using System.Domain.Entities;
using System.Infrastructure.Abstraction;

namespace System.Application.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<IEnumerable<Store>> GetDeletedStoresAsync();
        Task<bool> CreateStoreAsync(string storeName);
        Task<bool> UpdateStoreAsync(int id, string storeName);
        Task<bool> DeleteStoreAsync(int id);
        Task<bool> RestoreStoreAsync(int id);

        Task<IEnumerable<Branch>> GetBranchesByStoreIdAsync(int storeId);
        Task<bool> CreateBranchAsync(int storeId, string branchName);
        Task<bool> UpdateBranchAsync(int id, string branchName);
        Task<bool> DeleteBranchAsync(int id);

        Task<IEnumerable<Room>> GetRoomsByBranchIdAsync(int branchId);
        Task<bool> CreateRoomAsync(int branchId, string roomName);
        Task<bool> UpdateRoomAsync(int id, string roomName);
        Task<bool> DeleteRoomAsync(int id);

        Task<IEnumerable<Guest>> GetGuestsByRoomIdAsync(int roomId);
        Task<bool> CreateGuestAsync(int storeId, int branchId, int roomId, string username, string password, string phoneNumber);
        Task<bool> UpdateGuestAsync(string id, string username, string password, string phoneNumber);
        Task<bool> DeleteGuestAsync(string id);

        Task<IEnumerable<UserDto>> GetAllOwnersAsync();
        Task<bool> CreateOwnerAsync(string username, string password, string email, int storeId);
        Task<bool> UpdateOwnerAsync(string id, string username, string email);
        Task<bool> DeleteOwnerAsync(string id);
        Task<int?> GetOwnerStoreIdAsync(string ownerId);

        Task<bool> AdminLoginAsync(string username, string password);
        Task LogoutAsync();
    }
}