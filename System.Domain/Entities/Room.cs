using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Room : BaseEntity<int>
    {
        public string RoomName { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public List<Guest> Guests { get; set; } = [];
        public List<Order> Orders { get; set; } = [];
        public List<HelpRequest> HelpRequests { get; set; } = [];
    }
}