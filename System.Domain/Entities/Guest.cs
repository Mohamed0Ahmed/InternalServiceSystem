using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Guest : BaseEntity<string>
    {
        public int StoreId { get; set; }
        public int BranchId { get; set; }
        public int RoomId { get; set; }
        public required Store Store { get; set; }
        public Branch? Branch { get; set; }
        public required Room Room { get; set; }
    }
}