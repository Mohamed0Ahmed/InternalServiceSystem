using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : BaseEntityConfiguration<Customer, int>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.PhoneNumber)
                .IsRequired();

            builder.HasOne(c => c.Store)
                .WithMany()
                .HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.PhoneNumber, c.StoreId })
                .IsUnique();
        }
    }
}