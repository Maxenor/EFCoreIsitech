using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(10, 2);
            
        builder.Property(p => p.Description)
            .HasMaxLength(500);
            
        builder.Property(p => p.IsAvailable)
            .HasDefaultValue(true);
            
        // Relationships
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Configure computed properties
        builder.Ignore(p => p.AverageRating);
        builder.Ignore(p => p.ReviewCount);
        builder.Ignore(p => p.InStock);
            
        // Indexes
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.Price);
        
        // Seed data
        builder.HasData(
            new Product
            {
                Id = 1,
                Name = "Smartphone XYZ",
                Price = 599.99m,
                Description = "The latest smartphone with advanced features",
                ImageUrl = "/images/products/smartphone-xyz.jpg",
                IsAvailable = true,
                CategoryId = 4 // Smartphones category
            },
            new Product
            {
                Id = 2,
                Name = "Laptop Pro",
                Price = 1299.99m,
                Description = "High-performance laptop for professionals",
                ImageUrl = "/images/products/laptop-pro.jpg",
                IsAvailable = true,
                CategoryId = 5 // Laptops category
            },
            new Product
            {
                Id = 3,
                Name = "Classic Novel",
                Price = 14.99m,
                Description = "A timeless classic novel",
                ImageUrl = "/images/products/classic-novel.jpg",
                IsAvailable = true,
                CategoryId = 8 // Fiction category
            },
            new Product
            {
                Id = 4,
                Name = "Men's T-Shirt",
                Price = 24.99m,
                Description = "Comfortable cotton t-shirt",
                ImageUrl = "/images/products/mens-tshirt.jpg",
                IsAvailable = true,
                CategoryId = 6 // Men's clothing category
            },
            new Product
            {
                Id = 5,
                Name = "Women's Dress",
                Price = 49.99m,
                Description = "Elegant dress for any occasion",
                ImageUrl = "/images/products/womens-dress.jpg",
                IsAvailable = true,
                CategoryId = 7 // Women's clothing category
            }
        );
    }
}