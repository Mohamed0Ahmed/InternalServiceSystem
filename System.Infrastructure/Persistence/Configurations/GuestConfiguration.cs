using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(g => g.Username)
                .IsRequired();

            builder.Property(g => g.Password)
                .IsRequired();

            builder.HasOne(g => g.Store)
                .WithMany(s => s.Guests)
                .HasForeignKey(g => g.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Branch)
                .WithMany(b => b.Guests)
                .HasForeignKey(g => g.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Room)
                .WithMany(r => r.Guests)
                .HasForeignKey(g => g.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.Orders)
                .WithOne(o => o.Guest)
                .HasForeignKey(o => o.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.HelpRequests)
                .WithOne(h => h.Guest)
                .HasForeignKey(h => h.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(g => new { g.StoreId, g.Username }).IsUnique();

            builder.HasIndex(g => g.RoomId).IsUnique();

            builder.Property(g => g.SessionToken).IsRequired(false);
        }
    }
}