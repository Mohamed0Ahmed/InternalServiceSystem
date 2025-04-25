using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Domain.Entities;
using System.Infrastructure.Persistence.Identity;

namespace System.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<HelpRequest> HelpRequests { get; set; }
        public DbSet<PointsSetting> PointsSettings { get; set; }
        public DbSet<CustomerPoints> CustomerPoints { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Seed Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Owner", NormalizedName = "OWNER" },
                new IdentityRole { Id = "3", Name = "Guest", NormalizedName = "GUEST" },
                new IdentityRole { Id = "4", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            // Initialize PasswordHasher
            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed Admin User
            var adminUser = new ApplicationUser
            {
                Id = "admin-1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@playstationcafe.com",
                NormalizedEmail = "ADMIN@PLAYSTATIONCAFE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                StoreId = null // Admin doesn't need a StoreId
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");
            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign Admin Role to Admin User
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1", UserId = "admin-1" } // Admin Role for Admin
            );

         

      

       
        }
    }
}