using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Customer : BaseEntity<int>
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public List<Order> Orders { get; set; } = [];
        public List<HelpRequest> HelpRequests { get; set; } = [];
        public List<CustomerPoints> CustomerPoints { get; set; } = [];
    }
}