using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Store : BaseEntity<int>
    {
        public required string  StoreName { get; set; }
    }
}