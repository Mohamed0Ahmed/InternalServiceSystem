using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class HelpRequestConfiguration : BaseEntityConfiguration<HelpRequest, int>
    {
        public override void Configure(EntityTypeBuilder<HelpRequest> builder)
        {
            base.Configure(builder);

            builder.Property(hr => hr.RequestType)
                .IsRequired();

            builder.Property(hr => hr.Details);

            builder.Property(hr => hr.Status)
                .IsRequired();

            builder.HasOne(hr => hr.Customer)
                .WithMany()
                .HasForeignKey(hr => hr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(hr => hr.Guest)
                .WithMany()
                .HasForeignKey(hr => hr.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(hr => hr.Room)
                .WithMany()
                .HasForeignKey(hr => hr.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}