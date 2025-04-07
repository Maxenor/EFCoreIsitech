using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.OrderDate)
            .IsRequired();
            
        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<string>();
            
        builder.Property(o => o.TotalAmount)
            .IsRequired()
            .HasPrecision(10, 2);
            
        // Relationships
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder.HasIndex(o => o.OrderDate);
        builder.HasIndex(o => o.CustomerId);
        
        // Seed data
        builder.HasData(
            new Order
            {
                Id = 1,
                OrderDate = new DateTime(2025, 3, 15),
                CustomerId = 1,
                Status = OrderStatus.Delivered,
                TotalAmount = 624.98m
            },
            new Order
            {
                Id = 2,
                OrderDate = new DateTime(2025, 3, 20),
                CustomerId = 2,
                Status = OrderStatus.Shipped,
                TotalAmount = 1299.99m
            },
            new Order
            {
                Id = 3,
                OrderDate = new DateTime(2025, 4, 1),
                CustomerId = 1,
                Status = OrderStatus.Processing,
                TotalAmount = 74.98m
            }
        );
    }
}