using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class GuestPointsConfiguration : BaseEntityConfiguration<GuestPoints, int>
    {
        public override void Configure(EntityTypeBuilder<GuestPoints> builder)
        {
            base.Configure(builder);

            builder.Property(gp => gp.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(gp => gp.Points)
                .IsRequired();

            builder.HasOne(gp => gp.Branch)
                .WithMany()
                .HasForeignKey(gp => gp.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(gp => gp.Guest)
                .WithMany()
                .HasForeignKey(gp => gp.PhoneNumber)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(gp => new { gp.PhoneNumber, gp.BranchId })
                .IsUnique();
        }
    }
}