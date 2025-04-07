using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
{
    public void Configure(EntityTypeBuilder<ProductInventory> builder)
    {
        builder.ToTable("ProductInventories");
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.QuantityInStock)
            .IsRequired();
            
        builder.Property(i => i.SKU)
            .HasMaxLength(50);
            
        builder.Property(i => i.Location)
            .HasMaxLength(100);
            
        // One-to-one relationship with Product
        builder.HasOne(i => i.Product)
            .WithOne(p => p.Inventory)
            .HasForeignKey<ProductInventory>(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Indexes
        builder.HasIndex(i => i.SKU)
            .IsUnique()
            .HasFilter("SKU IS NOT NULL");
            
        // Seed data
        builder.HasData(
            new ProductInventory
            {
                Id = 1,
                ProductId = 1,
                QuantityInStock = 100,
                ReorderThreshold = 10,
                SKU = "PRD-001",
                Location = "Warehouse A, Shelf B3",
                LastRestockDate = new DateTime(2025, 3, 1),
                LastCountDate = new DateTime(2025, 4, 1),
                IsActive = true
            },
            new ProductInventory
            {
                Id = 2,
                ProductId = 2,
                QuantityInStock = 50,
                ReorderThreshold = 5,
                SKU = "PRD-002",
                Location = "Warehouse A, Shelf C4",
                LastRestockDate = new DateTime(2025, 3, 10),
                LastCountDate = new DateTime(2025, 4, 1),
                IsActive = true
            }
        );
    }
}