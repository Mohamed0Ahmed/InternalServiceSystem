using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class PointsSettingConfiguration : BaseEntityConfiguration<PointsSetting, int>
    {
        public override void Configure(EntityTypeBuilder<PointsSetting> builder)
        {
            base.Configure(builder);

            builder.Property(ps => ps.AmountPerPoint)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(ps => ps.PointsValue)
                .IsRequired();

            builder.HasOne(ps => ps.Branch)
                .WithMany()
                .HasForeignKey(ps => ps.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}