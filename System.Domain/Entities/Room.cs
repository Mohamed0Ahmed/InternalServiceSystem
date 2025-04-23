using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Room : BaseEntity<int>
    {
        public int BranchId { get; set; }
        public required string RoomName { get; set; }
        public Branch? Branch { get; set; }
    }
}