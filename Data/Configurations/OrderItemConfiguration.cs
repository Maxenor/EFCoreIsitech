using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        
        builder.HasKey(oi => oi.Id);
        
        builder.Property(oi => oi.Quantity)
            .IsRequired()
            .HasDefaultValue(1);
            
        builder.Property(oi => oi.UnitPrice)
            .IsRequired()
            .HasPrecision(10, 2);
            
        // Ignore computed property
        builder.Ignore(oi => oi.TotalPrice);
        
        // Relationships
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder.HasIndex(oi => new { oi.OrderId, oi.ProductId });
        
        // Seed data
        builder.HasData(
            // Order 1 items
            new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1,
                UnitPrice = 599.99m
            },
            new OrderItem
            {
                Id = 2,
                OrderId = 1,
                ProductId = 4,
                Quantity = 1,
                UnitPrice = 24.99m
            },
            
            // Order 2 items
            new OrderItem
            {
                Id = 3,
                OrderId = 2,
                ProductId = 2,
                Quantity = 1,
                UnitPrice = 1299.99m
            },
            
            // Order 3 items
            new OrderItem
            {
                Id = 4,
                OrderId = 3,
                ProductId = 3,
                Quantity = 2,
                UnitPrice = 14.99m
            },
            new OrderItem
            {
                Id = 5,
                OrderId = 3,
                ProductId = 4,
                Quantity = 2,
                UnitPrice = 24.99m
            }
        );
    }
}