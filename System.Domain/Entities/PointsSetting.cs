using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class PointsSetting : BaseEntity<int>
    {
        public int PointsPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}