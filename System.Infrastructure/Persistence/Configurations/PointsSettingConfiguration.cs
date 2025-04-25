using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class PointsSettingConfiguration : IEntityTypeConfiguration<PointsSetting>
    {
        public void Configure(EntityTypeBuilder<PointsSetting> builder)
        {
            builder.Property(ps => ps.PointsPerUnit)
                .IsRequired();

            builder.Property(ps => ps.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(ps => ps.Branch)
                .WithMany(b => b.PointsSettings)
                .HasForeignKey(ps => ps.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}