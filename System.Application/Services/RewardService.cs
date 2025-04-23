using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class RewardService : IRewardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RewardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Reward>> GetRewardsByBranchAsync(int branchId)
        {
            return await _unitOfWork.Repository<Reward, int>()
                .FindAsync(r => r.BranchId == branchId);
        }

        public async Task<Reward> CreateRewardAsync(int branchId, string name, int requiredPoints)
        {
            var reward = new Reward
            {
                BranchId = branchId,
                Name = name,
                RequiredPoints = requiredPoints
            };

            await _unitOfWork.Repository<Reward, int>().AddAsync(reward);
            await _unitOfWork.SaveChangesAsync();

            return reward;
        }
    }
}