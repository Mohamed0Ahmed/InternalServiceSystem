using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Customer : BaseEntity<int>
    {
        public required string PhoneNumber { get; set; }
        public int StoreId { get; set; }
        public required Store Store { get; set; }
    }
}