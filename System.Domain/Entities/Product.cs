using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsVisible { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public List<OrderItem> OrderItems { get; set; } = [];
    }
}