using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class HelpRequest : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public string GuestId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public Status Status { get; set; }
        public required Customer Customer { get; set; }
        public required Guest Guest { get; set; }
        public required Room Room { get; set; }
    }
}