using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public enum HelpRequestStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public class HelpRequest : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public string GuestId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public HelpRequestStatus Status { get; set; }
        public Customer Customer { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
    }
}