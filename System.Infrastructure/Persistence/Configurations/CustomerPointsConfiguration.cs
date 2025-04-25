using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class CustomerPointsConfiguration : IEntityTypeConfiguration<CustomerPoints>
    {
        public void Configure(EntityTypeBuilder<CustomerPoints> builder)
        {
            builder.Property(cp => cp.Points)
                .IsRequired();

            builder.HasOne(cp => cp.Customer)
                .WithMany(c => c.CustomerPoints)
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.Branch)
                .WithMany(b => b.CustomerPoints)
                .HasForeignKey(cp => cp.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}