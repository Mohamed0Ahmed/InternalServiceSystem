using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Branch : BaseEntity<int>
    {
        public string BranchName { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public List<Room> Rooms { get; set; } = [];
        public List<Guest> Guests { get; set; } = [];
        public List<Product> Products { get; set; } = [];
        public List<Reward> Rewards { get; set; } = [];
        public List<CustomerPoints> CustomerPoints { get; set; } = [];
        public List<PointsSetting> PointsSettings { get; set; } = [];
    }
}