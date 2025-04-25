using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class RewardConfiguration : IEntityTypeConfiguration<Reward>
    {
        public void Configure(EntityTypeBuilder<Reward> builder)
        {
            builder.Property(r => r.Name)
                .IsRequired();

            builder.Property(r => r.RequiredPoints)
                .IsRequired();

            builder.HasOne(r => r.Branch)
                .WithMany(b => b.Rewards)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}