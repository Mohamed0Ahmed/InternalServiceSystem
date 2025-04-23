using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class GuestPointsService : IGuestPointsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GuestPointsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GuestPoints> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<GuestPoints, int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<GuestPoints>> GetAllAsync()
        {
            return await _unitOfWork.Repository<GuestPoints, int>().GetAllAsync();
        }

        public async Task<GuestPoints> GetByGuestIdAndBranchIdAsync(string guestId, int branchId)
        {
            return (await _unitOfWork.Repository<GuestPoints, int>().FindAsync(gp => gp.PhoneNumber == guestId && gp.BranchId == branchId))
                .FirstOrDefault()!;
        }

        public async Task AddAsync(GuestPoints guestPoints)
        {
            var guest = await _unitOfWork.Repository<Guest, string>().GetByIdAsync(guestPoints.PhoneNumber);
            if (guest == null)
            {
                throw new InvalidOperationException($"Guest with ID {guestPoints.PhoneNumber} not found.");
            }

            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(guestPoints.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {guestPoints.BranchId} not found.");
            }

            await _unitOfWork.Repository<GuestPoints, int>().AddAsync(guestPoints);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(GuestPoints guestPoints)
        {
            // Business Logic: Check if the guest and branch exist
            var guest = await _unitOfWork.Repository<Guest, string>().GetByIdAsync(guestPoints.PhoneNumber);
            if (guest == null)
            {
                throw new InvalidOperationException($"Guest with ID {guestPoints.PhoneNumber} not found.");
            }

            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(guestPoints.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {guestPoints.BranchId} not found.");
            }

            _unitOfWork.Repository<GuestPoints, int>().Update(guestPoints);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var guestPoints = await _unitOfWork.Repository<GuestPoints, int>().GetByIdAsync(id);
            if (guestPoints == null)
            {
                throw new InvalidOperationException($"GuestPoints with ID {id} not found.");
            }

            _unitOfWork.Repository<GuestPoints, int>().Delete(guestPoints);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<int> CalculatePointsForOrderAsync(Order order, PointsSetting pointsSetting)
        {
            if (pointsSetting == null || pointsSetting.AmountPerPoint <= 0 || pointsSetting.AmountPerPoint <= 0)
            {
                return Task.FromResult(0);
            }

            decimal totalPrice = 5;
            int points = (int)(totalPrice / pointsSetting.AmountPerPoint);
            return Task.FromResult(points);
        }
    }
}