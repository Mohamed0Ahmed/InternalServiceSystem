using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GuestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guest> AuthenticateAsync(string username, string password, string storeName)
        {
            var store = await _unitOfWork.Repository<Store, int>()
                .GetAsync(s => s.StoreName == storeName);

            if (store == null)
            {
                throw new Exception("Store not found.");
            }

            var guest = await _unitOfWork.Repository<Guest, string>()
                .GetAsync(g => g.Username == username && g.Password == password && g.StoreId == store.Id);

            if (guest == null)
            {
                throw new Exception("Invalid username or password.");
            }

            return guest;
        }

        public async Task<Guest> GetGuestByIdAsync(string guestId)
        {
            var guest = await _unitOfWork.Repository<Guest, string>()
                .GetAsync(g => g.Id == guestId);

            if (guest == null)
            {
                throw new Exception("Guest not found.");
            }

            return guest;
        }
    }
}