using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GuestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guest> GetByIdAsync(string id)
        {
            return await _unitOfWork.Repository<Guest,string>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Guest>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Guest,string>().GetAllAsync();
        }

        public async Task<IEnumerable<Guest>> GetByBranchIdAsync(int branchId)
        {
            return await _unitOfWork.Repository<Guest, string>().FindAsync(g => g.BranchId == branchId);
        }

        public async Task AddAsync(Guest guest)
        {
            var store = await _unitOfWork.Repository<Room, int>().GetByIdAsync(guest.StoreId);
            if (store == null)
            {
                throw new InvalidOperationException($"Store with ID {guest.StoreId} not found.");
            }

            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(guest.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {guest.BranchId} not found.");
            }

            var room = await _unitOfWork.Repository<Room, int>().GetByIdAsync(guest.RoomId);
            if (room == null)
            {
                throw new InvalidOperationException($"Room with ID {guest.RoomId} not found.");
            }

            await _unitOfWork.Repository<Guest, string>().AddAsync(guest);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guest guest)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetByIdAsync(guest.StoreId);
            if (store == null)
            {
                throw new InvalidOperationException($"Store with ID {guest.StoreId} not found.");
            }

            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(guest.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {guest.BranchId} not found.");
            }

            var room = await _unitOfWork.Repository<Room, int>().GetByIdAsync(guest.RoomId);
            if (room == null)
            {
                throw new InvalidOperationException($"Room with ID {guest.RoomId} not found.");
            }

            _unitOfWork.Repository<Guest, string>().Update(guest);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var guest = await _unitOfWork.Repository<Guest, string>().GetByIdAsync(id);
            if (guest == null)
            {
                throw new InvalidOperationException($"Guest with ID {id} not found.");
            }

            _unitOfWork.Repository<Guest, string>().Delete(guest);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}