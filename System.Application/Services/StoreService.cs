using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Store> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Store,int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Store, int>().GetAllAsync();
        }

        public async Task AddAsync(Store store)
        {
            if (!await IsStoreNameUniqueAsync(store.StoreName))
            {
                throw new InvalidOperationException("Store name must be unique.");
            }

            await _unitOfWork.Repository<Store, int>().AddAsync(store);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Store store)
        {
            if (!await IsStoreNameUniqueAsync(store.StoreName, store.Id))
            {
                throw new InvalidOperationException("Store name must be unique.");
            }

            _unitOfWork.Repository<Store, int>().Update(store);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetByIdAsync(id) ?? throw new InvalidOperationException($"Store with ID {id} not found.");
            _unitOfWork.Repository<Store, int>().Delete(store);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsStoreNameUniqueAsync(string name, int? excludeId = null)
        {
            var existingStore = await _unitOfWork.Repository<Store,int>().GetAsync(s => s.StoreName == name);
            return existingStore == null || (excludeId.HasValue && existingStore.Id == excludeId.Value);
        }
    }
}