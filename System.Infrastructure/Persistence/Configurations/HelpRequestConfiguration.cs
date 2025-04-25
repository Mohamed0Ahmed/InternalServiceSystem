using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class HelpRequestConfiguration : IEntityTypeConfiguration<HelpRequest>
    {
        public void Configure(EntityTypeBuilder<HelpRequest> builder)
        {
            builder.Property(h => h.RequestType)
                .IsRequired();

            builder.Property(h => h.Details)
                .IsRequired();

            builder.HasOne(h => h.Customer)
                .WithMany(c => c.HelpRequests)
                .HasForeignKey(h => h.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Guest)
                .WithMany(g => g.HelpRequests)
                .HasForeignKey(h => h.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Room)
                .WithMany(r => r.HelpRequests)
                .HasForeignKey(h => h.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}