using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Room : BaseEntity<int>
    {
        public int BranchId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public Branch? Branch { get; set; }
    }
}