using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface ICustomerPointsService
    {
        Task<CustomerPoints> GetPointsAsync(int customerId, int branchId);
        Task UpdatePointsAsync(int customerId, int branchId, int points);
        Task RedeemPointsAsync(int customerId, int branchId, int rewardId);
    }
}