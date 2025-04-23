using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IRoomService
    {
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAllAsync();
        Task<IEnumerable<Room>> GetByBranchIdAsync(int branchId);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
        Task<bool> CheckAvailabilityAsync(int roomId);
    }
}