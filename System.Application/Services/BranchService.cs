using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Branch,int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Branch, int>().GetAllAsync();
        }

        public async Task<IEnumerable<Branch>> GetByStoreIdAsync(int storeId)
        {
            return await _unitOfWork.Repository<Branch, int>().FindAsync(b => b.StoreId == storeId);
        }

        public async Task AddAsync(Branch branch)
        {
            var store = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(branch.StoreId);
            if (store == null)
            {
                throw new InvalidOperationException($"Store with ID {branch.StoreId} not found.");
            }

            if (!await IsBranchNameUniqueAsync(branch.BranchName, branch.StoreId))
            {
                throw new InvalidOperationException("Branch name must be unique within the store.");
            }

            await _unitOfWork.Repository<Branch, int>().AddAsync(branch);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Branch branch)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetByIdAsync(branch.StoreId) ?? throw new InvalidOperationException($"Store with ID {branch.StoreId} not found.");

            if (!await IsBranchNameUniqueAsync(branch.BranchName, branch.StoreId, branch.Id))
            {
                throw new InvalidOperationException("Branch name must be unique within the store.");
            }

            _unitOfWork.Repository<Branch, int>().Update(branch);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(id);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {id} not found.");
            }

            _unitOfWork.Repository<Branch, int>().Delete(branch);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsBranchNameUniqueAsync(string name, int storeId, int? excludeId = null)
        {
            var existingBranch = await _unitOfWork.Repository<Branch,int>().GetAsync(b => b.BranchName == name && b.StoreId == storeId);
            return existingBranch == null || (excludeId.HasValue && existingBranch.Id == excludeId.Value);
        }
    }
}