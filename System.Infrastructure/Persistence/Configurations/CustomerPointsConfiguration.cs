using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class CustomerPointsConfiguration : BaseEntityConfiguration<CustomerPoints, int>
    {
        public override void Configure(EntityTypeBuilder<CustomerPoints> builder)
        {
            base.Configure(builder);

            builder.Property(cp => cp.Points)
                .IsRequired();

            builder.HasOne(cp => cp.Customer)
                .WithMany()
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.Branch)
                .WithMany()
                .HasForeignKey(cp => cp.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(cp => new { cp.CustomerId, cp.BranchId })
                .IsUnique();
        }
    }
}