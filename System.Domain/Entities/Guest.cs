using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Guest : BaseEntity<string>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public int BranchId { get; set; }
        public int RoomId { get; set; }
        public Store Store { get; set; }
        public Branch? Branch { get; set; }
        public Room Room { get; set; }
        public List<Order> Orders { get; set; } = [];
        public List<HelpRequest> HelpRequests { get; set; } = [];
        public string? SessionToken { get; set; }
    }
}