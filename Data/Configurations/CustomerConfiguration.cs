using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.Phone)
            .HasMaxLength(20);
            
        builder.Property(c => c.Address)
            .HasMaxLength(200);
            
        // Ignore computed property
        builder.Ignore(c => c.FullName);
        
        // Indexes
        builder.HasIndex(c => c.Email)
            .IsUnique();
            
        // Seed data
        builder.HasData(
            new Customer 
            { 
                Id = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john.doe@example.com",
                Phone = "555-123-4567",
                Address = "123 Main St, Anytown, AN 12345"
            },
            new Customer 
            { 
                Id = 2, 
                FirstName = "Jane", 
                LastName = "Smith", 
                Email = "jane.smith@example.com",
                Phone = "555-987-6543",
                Address = "456 Oak Ave, Somewhere, SW 67890"
            }
        );
    }
}