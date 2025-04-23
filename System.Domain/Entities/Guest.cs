using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Guest : BaseEntity<string>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int StoreId { get; set; }
        public int BranchId { get; set; }
        public int RoomId { get; set; }
        public required string PhoneNumber { get; set; }
        public required Store Store { get; set; }
        public Branch? Branch { get; set; }
        public required Room Room { get; set; }
    }
}