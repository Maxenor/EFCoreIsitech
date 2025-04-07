using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
{
    public void Configure(EntityTypeBuilder<ProductRating> builder)
    {
        builder.ToTable("ProductRatings");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Rating)
            .IsRequired();
            
        builder.Property(r => r.Review)
            .HasMaxLength(500);
            
        builder.Property(r => r.CreatedAt)
            .IsRequired();
            
        // Relationships
        builder.HasOne(r => r.Product)
            .WithMany(p => p.Ratings)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasIndex(r => new { r.CustomerId, r.ProductId })
            .IsUnique();
            
        builder.HasIndex(r => r.Rating);
        
        builder.HasData(
            new ProductRating
            {
                Id = 1,
                ProductId = 1,
                CustomerId = 1,
                Rating = 5,
                Review = "Excellent product, very satisfied with my purchase!",
                CreatedAt = new DateTime(2025, 3, 15),
                IsVerifiedPurchase = true,
                HelpfulVotes = 3
            },
            new ProductRating
            {
                Id = 2,
                ProductId = 1,
                CustomerId = 2,
                Rating = 4,
                Review = "Good quality but a bit expensive.",
                CreatedAt = new DateTime(2025, 3, 20),
                IsVerifiedPurchase = true,
                HelpfulVotes = 1
            }
        );
    }
}