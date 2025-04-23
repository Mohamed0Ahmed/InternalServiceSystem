using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Branch : BaseEntity<int>
    {
        public int StoreId { get; set; }
        public required string BranchName { get; set; }
        public required Store Store { get; set; }
    }
}