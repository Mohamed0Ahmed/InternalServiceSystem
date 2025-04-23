using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Branch : BaseEntity<int>
    {
        public int StoreId { get; set; }
        public  string BranchName { get; set; } = string.Empty;
        public  Store Store { get; set; }
    }
}