using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class PointsSettingService : IPointsSettingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PointsSettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PointsSetting> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<PointsSetting,int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<PointsSetting>> GetAllAsync()
        {
            return await _unitOfWork.Repository<PointsSetting, int>().GetAllAsync();
        }

        public async Task<IEnumerable<PointsSetting>> GetByBranchIdAsync(int branchId)
        {
            return await _unitOfWork.Repository<PointsSetting, int>().FindAsync(ps => ps.BranchId == branchId);
        }

        public async Task AddAsync(PointsSetting pointsSetting)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(pointsSetting.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {pointsSetting.BranchId} not found.");
            }

            await _unitOfWork.Repository<PointsSetting, int>().AddAsync(pointsSetting);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(PointsSetting pointsSetting)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(pointsSetting.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {pointsSetting.BranchId} not found.");
            }

            _unitOfWork.Repository<PointsSetting, int>().Update(pointsSetting);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pointsSetting = await _unitOfWork.Repository<PointsSetting, int>().GetByIdAsync(id);
            if (pointsSetting == null)
            {
                throw new InvalidOperationException($"PointsSetting with ID {id} not found.");
            }

            _unitOfWork.Repository<PointsSetting, int>().Delete(pointsSetting);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}