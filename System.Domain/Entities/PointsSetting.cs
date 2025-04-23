using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class PointsSetting : BaseEntity<int>
    {
        public int BranchId { get; set; }
        public decimal AmountPerPoint { get; set; }
        public int PointsValue { get; set; }
        public  Branch Branch { get; set; }
    }
}