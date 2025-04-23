using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class GuestPoints : BaseEntity<int>
    {
        public required string PhoneNumber { get; set; }
        public int BranchId { get; set; }
        public int Points { get; set; }
        public Guest? Guest { get; set; }
        public Branch? Branch { get; set; }

    }
}