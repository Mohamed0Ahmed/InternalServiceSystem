using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : BaseEntityConfiguration<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.TotalPriceAtOrderTime)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status)
                .IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Guest)
                .WithMany()
                .HasForeignKey(o => o.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Room)
                .WithMany()
                .HasForeignKey(o => o.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}