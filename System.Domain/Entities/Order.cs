using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public string GuestId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public decimal TotalPriceAtOrderTime { get; set; }
        public DateTime OrderDate { get; set; }
        public Status Status { get; set; }
        public  Customer Customer { get; set; }
        public  Guest Guest { get; set; }
        public  Room Room { get; set; }
        public  List<OrderItem> OrderItems { get; set; }
    }

    public enum Status
    {
        Pending = 0,
        Done = 1,
        Canceled = 2
    }
}