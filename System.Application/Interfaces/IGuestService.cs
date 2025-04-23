using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IGuestService
    {
        Task<Guest> GetByIdAsync(string id);
        Task<IEnumerable<Guest>> GetAllAsync();
        Task<IEnumerable<Guest>> GetByBranchIdAsync(int branchId);
        Task AddAsync(Guest guest);
        Task UpdateAsync(Guest guest);
        Task DeleteAsync(string id);
    }
}