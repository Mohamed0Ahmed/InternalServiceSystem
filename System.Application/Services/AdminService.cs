using System.Domain.Entities;
using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Infrastructure.Abstraction;

namespace System.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return await _unitOfWork.Repository<Store, int>().FindAllAsync(s => !s.IsDeleted && !s.IsHidden);
        }

        public async Task<IEnumerable<Store>> GetDeletedStoresAsync()
        {
            return await _unitOfWork.Repository<Store, int>()
                .FindAllAsync(s => s.IsDeleted && !s.IsHidden);
        }

        public async Task<bool> CreateStoreAsync(string storeName)
        {
            if (string.IsNullOrWhiteSpace(storeName))
                return false;

            var store = new Store
            {
                StoreName = storeName,
                IsDeleted = false,
                IsHidden = false
            };

            await _unitOfWork.Repository<Store, int>().AddAsync(store);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStoreAsync(int id, string storeName)
        {
            if (string.IsNullOrWhiteSpace(storeName))
                return false;

            var store = await _unitOfWork.Repository<Store, int>().GetAsync(s=>s.Id == id);
            if (store == null || store.IsDeleted || store.IsHidden)
                return false;

            store.StoreName = storeName;
            _unitOfWork.Repository<Store, int>().Update(store);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStoreAsync(int id)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetAsync(s=>s.Id== id);
            if (store == null || store.IsDeleted || store.IsHidden)
                return false;

            store.IsDeleted = true;
            _unitOfWork.Repository<Store, int>().Update(store);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RestoreStoreAsync(int id)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetAsync(s => s.Id == id);
            if (store == null || !store.IsDeleted || store.IsHidden)
                return false;

            store.IsDeleted = false;
            _unitOfWork.Repository<Store, int>().Update(store);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Branch>> GetBranchesByStoreIdAsync(int storeId)
        {
            return await _unitOfWork.Repository<Branch, int>()
                .FindAllAsync(b => b.StoreId == storeId && !b.IsDeleted && !b.IsHidden);
        }

        public async Task<bool> CreateBranchAsync(int storeId, string branchName)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                return false;

            var store = await _unitOfWork.Repository<Store, int>().GetAsync(s=>s.Id == storeId);
            if (store == null || store.IsDeleted || store.IsHidden)
                return false;

            var branch = new Branch
            {
                StoreId = storeId,
                BranchName = branchName,
                IsDeleted = false,
                IsHidden = false
            };

            await _unitOfWork.Repository<Branch, int>().AddAsync(branch);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBranchAsync(int id, string branchName)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                return false;

            var branch = await _unitOfWork.Repository<Branch, int>().GetAsync(b => b.Id == id);
            if (branch == null || branch.IsDeleted || branch.IsHidden)
                return false;

            branch.BranchName = branchName;
            _unitOfWork.Repository<Branch, int>().Update(branch);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetAsync(b => b.Id == id);
            if (branch == null || branch.IsDeleted || branch.IsHidden)
                return false;

            branch.IsDeleted = true;
            _unitOfWork.Repository<Branch, int>().Update(branch);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        // Room Management
        public async Task<IEnumerable<Room>> GetRoomsByBranchIdAsync(int branchId)
        {
            return await _unitOfWork.Repository<Room, int>()
                .FindAllAsync(r => r.BranchId == branchId && !r.IsDeleted && !r.IsHidden);
        }

        public async Task<bool> CreateRoomAsync(int branchId, string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
                return false;

            var branch = await _unitOfWork.Repository<Branch, int>().GetAsync(b => b.Id == branchId);
            if (branch == null || branch.IsDeleted || branch.IsHidden)
                return false;

            var room = new Room
            {
                BranchId = branchId,
                RoomName = roomName,
                IsDeleted = false,
                IsHidden = false
            };

            await _unitOfWork.Repository<Room, int>().AddAsync(room);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRoomAsync(int id, string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
                return false;

            var room = await _unitOfWork.Repository<Room, int>().GetAsync(r => r.Id == id);
            if (room == null || room.IsDeleted || room.IsHidden)
                return false;

            room.RoomName = roomName;
            _unitOfWork.Repository<Room, int>().Update(room);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _unitOfWork.Repository<Room, int>().GetAsync(r => r.Id == id);
            if (room == null || room.IsDeleted || room.IsHidden)
                return false;

            room.IsDeleted = true;
            _unitOfWork.Repository<Room, int>().Update(room);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Guest>> GetGuestsByRoomIdAsync(int roomId)
        {
            return await _unitOfWork.Repository<Guest, string>()
                .FindAllAsync(g => g.RoomId == roomId && !g.IsDeleted && !g.IsHidden);
        }

        public async Task<bool> CreateGuestAsync(int storeId, int branchId, int roomId, string username, string password, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            var room = await _unitOfWork.Repository<Room, int>().GetAsync(r => r.Id == roomId);
            if (room == null || room.IsDeleted || room.IsHidden)
                return false;

            var branch = await _unitOfWork.Repository<Branch, int>().GetAsync(b=>b.Id==branchId);
            if (branch == null || branch.IsDeleted || branch.IsHidden || branch.Id != room.BranchId)
                return false;

            var store = await _unitOfWork.Repository<Store, int>().GetAsync(s=>s.Id==storeId);
            if (store == null || store.IsDeleted || store.IsHidden || store.Id != branch.StoreId)
                return false;

            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var guest = new Guest
            {
                StoreId = storeId,
                BranchId = branchId,
                RoomId = roomId,
                Username = username,
                Password = password,
                PhoneNumber = phoneNumber,
                IsDeleted = false,
                IsHidden = false
            };

            await _unitOfWork.Repository<Guest, string>().AddAsync(guest);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGuestAsync(string id, string username, string password, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            var guest = await _unitOfWork.Repository<Guest, string>().GetAsync(g=>g.Id == id);
            if (guest == null || guest.IsDeleted || guest.IsHidden)
                return false;

            guest.Username = username;
            //guest.Password = BCrypt.Net.BCrypt.HashPassword(password);
            guest.Password = password;
            guest.PhoneNumber = phoneNumber;

            _unitOfWork.Repository<Guest, string>().Update(guest);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGuestAsync(string id)
        {
            var guest = await _unitOfWork.Repository<Guest, string>().GetAsync(g=>g.Id == id);
            if (guest == null || guest.IsDeleted || guest.IsHidden)
                return false;

            guest.IsDeleted = true;
            _unitOfWork.Repository<Guest, string>().Update(guest);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UserDto>> GetAllOwnersAsync()
        {
            return await _identityService.GetAllOwnersAsync();
        }

        public async Task<bool> CreateOwnerAsync(string username, string password, string email, int storeId)
        {
            return await _identityService.CreateOwnerAsync(username, password, email, storeId);
        }

        public async Task<bool> UpdateOwnerAsync(string id, string username, string email)
        {
            return await _identityService.UpdateOwnerAsync(id, username, email);
        }

        public async Task<bool> DeleteOwnerAsync(string id)
        {
            return await _identityService.DeleteOwnerAsync(id);
        }

        public async Task<int?> GetOwnerStoreIdAsync(string ownerId)
        {
            return await _identityService.GetOwnerStoreIdAsync(ownerId);
        }

        public async Task<bool> AdminLoginAsync(string username, string password)
        {
            return await _identityService.AdminLoginAsync(username, password);
        }

        public async Task LogoutAsync()
        {
            await _identityService.LogoutAsync();
        }

     
    }
}