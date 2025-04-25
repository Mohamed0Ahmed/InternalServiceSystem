using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Guest)
                .WithMany(g => g.Orders)
                .HasForeignKey(o => o.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Room)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}