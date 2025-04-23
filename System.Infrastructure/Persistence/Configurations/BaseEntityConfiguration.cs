using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Shared.BaseModel;

namespace System.Infrastructure.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<T, TID> : IEntityTypeConfiguration<T> where T : BaseEntity<TID> where TID : IEquatable<TID>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);


            builder.Property(e => e.Id)
                .IsRequired();
           

            builder.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.IsHidden)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.CreatedOn)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.CreatedBy)
                .IsRequired(false);

            builder.Property(e => e.LastModifiedOn)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.LastModifiedBy)
                .IsRequired(false);

            builder.Property(e => e.DeletedOn)
                .IsRequired(false);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasQueryFilter(e => !e.IsHidden);

            builder.HasIndex(e => e.CreatedOn);
            builder.HasIndex(e => e.LastModifiedOn);
        }
    }
}