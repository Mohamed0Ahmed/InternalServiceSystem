using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomsByBranchAsync(int branchId);
    }
}