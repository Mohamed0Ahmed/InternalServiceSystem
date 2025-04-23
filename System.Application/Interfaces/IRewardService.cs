using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IRewardService
    {
        Task<IEnumerable<Reward>> GetRewardsByBranchAsync(int branchId);
        Task<Reward> CreateRewardAsync(int branchId, string name, int requiredPoints);
    }
}