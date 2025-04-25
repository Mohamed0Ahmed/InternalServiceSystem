using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(r => r.RoomName)
                .IsRequired();

            builder.HasOne(r => r.Branch)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Guests)
                .WithOne(g => g.Room)
                .HasForeignKey(g => g.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Orders)
                .WithOne(o => o.Room)
                .HasForeignKey(o => o.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.HelpRequests)
                .WithOne(h => h.Room)
                .HasForeignKey(h => h.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}