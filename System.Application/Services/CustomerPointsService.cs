using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class CustomerPointsService : ICustomerPointsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerPointsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerPoints> GetPointsAsync(int customerId, int branchId)
        {
            var points = await _unitOfWork.Repository<CustomerPoints, int>()
                .GetAsync(cp => cp.CustomerId == customerId && cp.BranchId == branchId);

            if (points == null)
            {
                points = new CustomerPoints
                {
                    CustomerId = customerId,
                    BranchId = branchId,
                    Points = 0
                };
                await _unitOfWork.Repository<CustomerPoints, int>().AddAsync(points);
                await _unitOfWork.SaveChangesAsync();
            }

            return points;
        }

        public async Task UpdatePointsAsync(int customerId, int branchId, int points)
        {
            var customerPoints = await GetPointsAsync(customerId, branchId);
            customerPoints.Points += points;
            _unitOfWork.Repository<CustomerPoints, int>().Update(customerPoints);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RedeemPointsAsync(int customerId, int branchId, int rewardId)
        {
            var customerPoints = await GetPointsAsync(customerId, branchId);
            var reward = await _unitOfWork.Repository<Reward, int>()
                .GetAsync(r => r.Id == rewardId && r.BranchId == branchId);

            if (reward == null)
            {
                throw new Exception("Reward not found.");
            }

            if (customerPoints.Points < reward.RequiredPoints)
            {
                throw new Exception("Not enough points to redeem this reward.");
            }

            customerPoints.Points -= reward.RequiredPoints;
            _unitOfWork.Repository<CustomerPoints, int>().Update(customerPoints);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}