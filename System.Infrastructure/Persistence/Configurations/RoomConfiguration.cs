using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : BaseEntityConfiguration<Room, int>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.RoomName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(r => r.Branch)
                .WithMany()
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}