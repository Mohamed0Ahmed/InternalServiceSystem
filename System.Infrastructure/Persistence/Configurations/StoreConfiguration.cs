using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : BaseEntityConfiguration<Store, int>
    {
        public override void Configure(EntityTypeBuilder<Store> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.StoreName)
                .IsRequired();
        }
    }
}