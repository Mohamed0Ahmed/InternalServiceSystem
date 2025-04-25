using Microsoft.AspNetCore.Identity;

namespace System.Infrastructure.Persistence.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int? StoreId { get; set; }
    }
}
