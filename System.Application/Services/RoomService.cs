using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Room>> GetRoomsByBranchAsync(int branchId)
        {
            return await _unitOfWork.Repository<Room, int>()
                .FindAsync(r => r.BranchId == branchId);
        }
    }
}