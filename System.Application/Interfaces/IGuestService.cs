using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IGuestService
    {
        Task<Guest> AuthenticateAsync(string username, string password, string storeName);
        Task<Guest> GetGuestByIdAsync(string guestId);
    }
}