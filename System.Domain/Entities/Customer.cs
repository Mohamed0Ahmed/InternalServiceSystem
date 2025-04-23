using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Customer : BaseEntity<int>
    {
        public  string PhoneNumber { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public  Store? Store { get; set; } 
    }
}