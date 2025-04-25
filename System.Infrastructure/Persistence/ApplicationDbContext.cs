using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Domain.Entities;
using System.Infrastructure.Persistence.Identity;
using System.Shared.BaseModel;

namespace System.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }



        public DbSet<Store> Stores { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<HelpRequest> HelpRequests { get; set; }
        public DbSet<CustomerPoints> CustomerPoints { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<PointsSetting> PointsSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(ApplicationDbContext)
                        .GetMethod(nameof(ApplySoftDeleteFilter), Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance)!
                        .MakeGenericMethod(entityType.ClrType);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        private static void  ApplySoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : class, IEntity
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted && !e.IsHidden);
        }

      
    }
}