using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class ProductPriceHistoryConfiguration : IEntityTypeConfiguration<ProductPriceHistory>
{
    public void Configure(EntityTypeBuilder<ProductPriceHistory> builder)
    {
        builder.ToTable("ProductPriceHistories");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
            
        builder.Property(p => p.EffectiveDate)
            .IsRequired();
            
        builder.Property(p => p.Reason)
            .HasMaxLength(255);
            
        builder.Property(p => p.ChangedBy)
            .HasMaxLength(50);
            
        builder.HasOne(p => p.Product)
            .WithMany(p => p.PriceHistory)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(p => p.EffectiveDate);
        builder.HasIndex(p => new { p.ProductId, p.EffectiveDate });
        
        builder.HasData(
            new ProductPriceHistory
            {
                Id = 1,
                ProductId = 1,
                Price = 19.99m,
                EffectiveDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 2, 28),
                Reason = "Initial price",
                ChangedBy = "System"
            },
            new ProductPriceHistory
            {
                Id = 2,
                ProductId = 1,
                Price = 24.99m,
                EffectiveDate = new DateTime(2025, 3, 1),
                EndDate = null,
                Reason = "Price increase due to high demand",
                ChangedBy = "Admin"
            },
            new ProductPriceHistory
            {
                Id = 3,
                ProductId = 2,
                Price = 29.99m,
                EffectiveDate = new DateTime(2025, 1, 1),
                EndDate = null,
                Reason = "Initial price",
                ChangedBy = "System"
            }
        );
    }
}