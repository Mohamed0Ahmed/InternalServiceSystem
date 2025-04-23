using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Reward : BaseEntity<int>
    {
        public int BranchId { get; set; }
        public required string Name { get; set; }
        public int RequiredPoints { get; set; }
        public required Branch Branch { get; set; }
    }
}