using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IStoreService
    {
        Task<Store> GetByIdAsync(int id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task DeleteAsync(int id);
        Task<bool> IsStoreNameUniqueAsync(string name, int? excludeId = null);
    }
}