using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IHelpRequestService
    {
        Task<HelpRequest> CreateHelpRequestAsync(int customerId, string guestId, int roomId, string requestType, string details);
        Task<IEnumerable<HelpRequest>> GetPendingHelpRequestsAsync();
        Task ConfirmHelpRequestAsync(int helpRequestId);
        Task CancelHelpRequestAsync(int helpRequestId);
    }
}