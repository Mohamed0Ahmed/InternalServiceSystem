using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Store : BaseEntity<int>
    {
        public string StoreName { get; set; } = string.Empty;
    }
}