using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class RewardConfiguration : BaseEntityConfiguration<Reward, int>
    {
        public override void Configure(EntityTypeBuilder<Reward> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Name)
                .IsRequired();

            builder.Property(r => r.RequiredPoints)
                .IsRequired();

            builder.HasOne(r => r.Branch)
                .WithMany()
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}