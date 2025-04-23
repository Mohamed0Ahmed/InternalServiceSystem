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

        public async Task<HelpRequest> CreateHelpRequestAsync(int customerId, string guestId, int roomId, string requestType, string details)
        {
            var helpRequest = new HelpRequest
            {
                CustomerId = customerId,
                GuestId = guestId,
                RoomId = roomId,
                RequestType = requestType,
                Details = details,
                RequestDate = DateTime.UtcNow,
                Status = Status.Pending
            };

            await _unitOfWork.Repository<HelpRequest, int>().AddAsync(helpRequest);
            await _unitOfWork.SaveChangesAsync();

            return helpRequest;
        }

        public async Task<IEnumerable<HelpRequest>> GetPendingHelpRequestsAsync()
        {
            return await _unitOfWork.Repository<HelpRequest, int>()
                .FindAsync(hr => hr.Status == Status.Pending);
        }

        public async Task ConfirmHelpRequestAsync(int helpRequestId)
        {
            var helpRequest = await _unitOfWork.Repository<HelpRequest, int>()
                .GetAsync(hr => hr.Id == helpRequestId);

            if (helpRequest == null)
            {
                throw new Exception("Help request not found.");
            }

            helpRequest.Status = Status.Done;
            _unitOfWork.Repository<HelpRequest, int>().Update(helpRequest);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelHelpRequestAsync(int helpRequestId)
        {
            var helpRequest = await _unitOfWork.Repository<HelpRequest, int>()
                .GetAsync(hr => hr.Id == helpRequestId);

            if (helpRequest == null)
            {
                throw new Exception("Help request not found.");
            }

            helpRequest.Status = Status.Canceled;
            _unitOfWork.Repository<HelpRequest, int>().Update(helpRequest);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}