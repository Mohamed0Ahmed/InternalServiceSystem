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

            builder.Property(hr => hr.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(hr => hr.RequestType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(hr => hr.Details)
                .HasMaxLength(500);

            builder.Property(hr => hr.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(hr => hr.Guest)
                .WithMany()
                .HasForeignKey(hr => hr.PhoneNumber)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(hr => hr.Room)
                .WithMany()
                .HasForeignKey(hr => hr.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}