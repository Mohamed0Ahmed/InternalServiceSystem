using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IBranchService
    {
        Task<Branch> GetByIdAsync(int id);
        Task<IEnumerable<Branch>> GetAllAsync();
        Task<IEnumerable<Branch>> GetByStoreIdAsync(int storeId);
        Task AddAsync(Branch branch);
        Task UpdateAsync(Branch branch);
        Task DeleteAsync(int id);
        Task<bool> IsBranchNameUniqueAsync(string name, int storeId, int? excludeId = null);
    }
}