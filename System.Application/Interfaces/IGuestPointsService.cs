using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IGuestPointsService
    {
        Task<GuestPoints> GetByIdAsync(int id);
        Task<IEnumerable<GuestPoints>> GetAllAsync();
        Task<GuestPoints> GetByGuestIdAndBranchIdAsync(string guestId, int branchId);
        Task AddAsync(GuestPoints guestPoints);
        Task UpdateAsync(GuestPoints guestPoints);
        Task DeleteAsync(int id);
        Task<int> CalculatePointsForOrderAsync(Order order, PointsSetting pointsSetting);
    }
}