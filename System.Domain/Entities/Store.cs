using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Store : BaseEntity<int>
    {
        public string StoreName { get; set; } = string.Empty;
        public List<Branch> Branches { get; set; } = [];
        public List<Guest> Guests { get; set; } = [];
        public List<Customer> Customers { get; set; } = [];
    }
}