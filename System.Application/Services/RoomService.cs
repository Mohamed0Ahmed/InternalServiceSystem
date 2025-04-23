using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Room,int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Room, int>().GetAllAsync();
        }

        public async Task<IEnumerable<Room>> GetByBranchIdAsync(int branchId)
        {
            return await _unitOfWork.Repository<Room, int>().FindAsync(r => r.BranchId == branchId);
        }

        public async Task AddAsync(Room room)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(room.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {room.BranchId} not found.");
            }

            await _unitOfWork.Repository<Room, int>().AddAsync(room);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            var branch = await _unitOfWork.Repository<Room, int>().GetByIdAsync(room.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {room.BranchId} not found.");
            }

            _unitOfWork.Repository<Room, int>().Update(room);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _unitOfWork.Repository<Room, int>().GetByIdAsync(id);
            if (room == null)
            {
                throw new InvalidOperationException($"Room with ID {id} not found.");
            }

            _unitOfWork.Repository<Room, int>().Delete(room);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CheckAvailabilityAsync(int roomId)
        {
            var activeOrders = await _unitOfWork.Repository<Order,int>().FindAsync(o => o.RoomId == roomId && !o.IsDeleted);
            return !activeOrders.Any();
        }
    }
}