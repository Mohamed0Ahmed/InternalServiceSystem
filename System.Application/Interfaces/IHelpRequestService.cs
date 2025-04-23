using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IHelpRequestService
    {
        Task<HelpRequest> GetByIdAsync(int id);
        Task<IEnumerable<HelpRequest>> GetAllAsync();
        Task<IEnumerable<HelpRequest>> GetByGuestIdAsync(string guestId);
        Task AddAsync(HelpRequest helpRequest);
        Task UpdateAsync(HelpRequest helpRequest);
        Task DeleteAsync(int id);
    }
}