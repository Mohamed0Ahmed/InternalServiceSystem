using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public class Order : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public string GuestId { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public Customer Customer { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public List<OrderItem> OrderItems { get; set; } = [];
    }
}