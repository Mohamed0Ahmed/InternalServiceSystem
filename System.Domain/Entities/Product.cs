using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public int BranchId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsVisible { get; set; }
        public Branch? Branch { get; set; }
    }
}