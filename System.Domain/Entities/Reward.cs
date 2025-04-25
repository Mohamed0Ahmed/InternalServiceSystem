using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Reward : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int RequiredPoints { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
