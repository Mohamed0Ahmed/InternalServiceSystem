using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        public required string PhoneNumber { get; set; }
        public int RoomId { get; set; }
        public DateTime OrderDate { get; set; }
        public Status Status { get; set; }
        public required Guest Guest { get; set; }
        public required Room Room { get; set; }
        public required List<OrderItem> OrderItems { get; set; }
    }



    public enum Status {
        Pending = 0,
        Done = 1,
        Canceled = 2,

    }
}


