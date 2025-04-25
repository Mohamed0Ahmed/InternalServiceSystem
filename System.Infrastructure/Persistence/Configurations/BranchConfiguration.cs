using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(b => b.BranchName)
                .IsRequired();

            builder.HasOne(b => b.Store)
                .WithMany(s => s.Branches)
                .HasForeignKey(b => b.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Rooms)
                .WithOne(r => r.Branch)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Guests)
                .WithOne(g => g.Branch)
                .HasForeignKey(g => g.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Products)
                .WithOne(p => p.Branch)
                .HasForeignKey(p => p.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Rewards)
                .WithOne(r => r.Branch)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.CustomerPoints)
                .WithOne(cp => cp.Branch)
                .HasForeignKey(cp => cp.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.PointsSettings)
                .WithOne(ps => ps.Branch)
                .HasForeignKey(ps => ps.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}