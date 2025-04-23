using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IPointsSettingService
    {
        Task<PointsSetting> GetByIdAsync(int id);
        Task<IEnumerable<PointsSetting>> GetAllAsync();
        Task<IEnumerable<PointsSetting>> GetByBranchIdAsync(int branchId);
        Task AddAsync(PointsSetting pointsSetting);
        Task UpdateAsync(PointsSetting pointsSetting);
        Task DeleteAsync(int id);
    }
}