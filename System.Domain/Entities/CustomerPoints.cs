using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class CustomerPoints : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public int Points { get; set; }
        public required Customer Customer { get; set; }
        public required Branch Branch { get; set; }
    }
}