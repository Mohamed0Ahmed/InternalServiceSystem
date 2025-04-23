using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class GuestConfiguration : BaseEntityConfiguration<Guest, string>
    {
        public override void Configure(EntityTypeBuilder<Guest> builder)
        {
            base.Configure(builder);

            builder.HasOne(g => g.Store)
                .WithMany()
                .HasForeignKey(g => g.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Branch)
                .WithMany()
                .HasForeignKey(g => g.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Room)
                .WithMany()
                .HasForeignKey(g => g.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}