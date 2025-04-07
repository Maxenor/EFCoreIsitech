using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(20);
            
        builder.Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(a => a.State)
            .HasMaxLength(50);
            
        builder.Property(a => a.AdditionalInfo)
            .HasMaxLength(200);
            
        // Relationship with Customer
        builder.HasOne(a => a.Customer)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Create composite index
        builder.HasIndex(a => new { a.CustomerId, a.Type, a.IsDefault });
        
        // Seed data
        builder.HasData(
            new Address 
            { 
                Id = 1, 
                Street = "123 Main St",
                City = "Anytown",
                PostalCode = "12345",
                Country = "USA",
                State = "AN",
                Type = AddressType.Billing,
                CustomerId = 1,
                IsDefault = true
            },
            new Address 
            { 
                Id = 2, 
                Street = "123 Main St",
                City = "Anytown",
                PostalCode = "12345",
                Country = "USA",
                State = "AN",
                Type = AddressType.Shipping,
                CustomerId = 1,
                IsDefault = true
            },
            new Address 
            { 
                Id = 3, 
                Street = "456 Oak Ave",
                City = "Somewhere",
                PostalCode = "67890",
                Country = "USA",
                State = "SW",
                Type = AddressType.Billing,
                CustomerId = 2,
                IsDefault = true
            },
            new Address 
            { 
                Id = 4, 
                Street = "456 Oak Ave",
                City = "Somewhere",
                PostalCode = "67890",
                Country = "USA",
                State = "SW",
                Type = AddressType.Shipping,
                CustomerId = 2,
                IsDefault = true
            }
        );
    }
}