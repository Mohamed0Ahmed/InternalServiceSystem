using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(s => s.StoreName)
                .IsRequired();

            builder.HasMany(s => s.Branches)
                .WithOne(b => b.Store)
                .HasForeignKey(b => b.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Guests)
                .WithOne(g => g.Store)
                .HasForeignKey(g => g.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Customers)
                .WithOne(c => c.Store)
                .HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => s.StoreName).IsUnique();
        }
    }
}