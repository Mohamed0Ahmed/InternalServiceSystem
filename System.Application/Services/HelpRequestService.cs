using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class HelpRequestService : IHelpRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HelpRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HelpRequest> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<HelpRequest,int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<HelpRequest>> GetAllAsync()
        {
            return await _unitOfWork.Repository<HelpRequest,int>().GetAllAsync();
        }

        public async Task<IEnumerable<HelpRequest>> GetByGuestIdAsync(string guestId)
        {
            return await _unitOfWork.Repository<HelpRequest, int>().FindAsync(hr => hr.PhoneNumber == guestId);
        }

        public async Task AddAsync(HelpRequest helpRequest)
        {
            var guest = await _unitOfWork.Repository<Guest, string>().GetByIdAsync(helpRequest.PhoneNumber);
            if (guest == null)
            {
                throw new InvalidOperationException($"Guest with ID {helpRequest.PhoneNumber} not found.");
            }

            await _unitOfWork.Repository<HelpRequest, int>().AddAsync(helpRequest);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(HelpRequest helpRequest)
        {
            var guest = await _unitOfWork.Repository<Guest, string>().GetByIdAsync(helpRequest.PhoneNumber);
            if (guest == null)
            {
                throw new InvalidOperationException($"Guest with ID {helpRequest.PhoneNumber} not found.");
            }

            _unitOfWork.Repository<HelpRequest, int>().Update(helpRequest);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var helpRequest = await _unitOfWork.Repository<HelpRequest, int>().GetByIdAsync(id);
            if (helpRequest == null)
            {
                throw new InvalidOperationException($"HelpRequest with ID {id} not found.");
            }

            _unitOfWork.Repository<HelpRequest, int>().Delete(helpRequest);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}