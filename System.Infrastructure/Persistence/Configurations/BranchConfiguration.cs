using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class BranchConfiguration : BaseEntityConfiguration<Branch, int>
    {
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(b => b.Store)
                .WithMany()
                .HasForeignKey(b => b.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}