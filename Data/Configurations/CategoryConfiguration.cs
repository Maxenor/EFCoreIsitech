using EFCoreIsitech.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreIsitech.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.Description)
            .HasMaxLength(500);
            
        builder.Property(c => c.Slug)
            .HasMaxLength(100);
            
        builder.HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
            
        builder.Ignore(c => c.FullPath);
            
        builder.HasIndex(c => c.Name);
        builder.HasIndex(c => c.Slug)
            .IsUnique()
            .HasFilter("Slug IS NOT NULL");
        builder.HasIndex(c => c.ParentCategoryId);
            
        builder.HasData(
            // Main categories
            new Category { 
                Id = 1, 
                Name = "Electronics", 
                Description = "Electronic devices and accessories",
                Slug = "electronics",
                SortOrder = 1,
                IsActive = true
            },
            new Category { 
                Id = 2, 
                Name = "Clothing", 
                Description = "Apparel and fashion items",
                Slug = "clothing",
                SortOrder = 2,
                IsActive = true
            },
            new Category { 
                Id = 3, 
                Name = "Books", 
                Description = "Books and publications",
                Slug = "books",
                SortOrder = 3,
                IsActive = true
            },
            
            // Sub-categories for Electronics
            new Category { 
                Id = 4, 
                Name = "Smartphones", 
                Description = "Mobile phones and accessories",
                ParentCategoryId = 1,
                Slug = "smartphones",
                SortOrder = 1,
                IsActive = true
            },
            new Category { 
                Id = 5, 
                Name = "Laptops", 
                Description = "Notebook computers",
                ParentCategoryId = 1,
                Slug = "laptops",
                SortOrder = 2,
                IsActive = true
            },
            
            // Sub-categories for Clothing
            new Category { 
                Id = 6, 
                Name = "Men's", 
                Description = "Men's clothing",
                ParentCategoryId = 2,
                Slug = "mens",
                SortOrder = 1,
                IsActive = true
            },
            new Category { 
                Id = 7, 
                Name = "Women's", 
                Description = "Women's clothing",
                ParentCategoryId = 2,
                Slug = "womens",
                SortOrder = 2,
                IsActive = true
            },
            
            // Sub-categories for Books
            new Category { 
                Id = 8, 
                Name = "Fiction", 
                Description = "Fictional literature",
                ParentCategoryId = 3,
                Slug = "fiction",
                SortOrder = 1,
                IsActive = true
            },
            new Category { 
                Id = 9, 
                Name = "Non-Fiction", 
                Description = "Non-fictional literature",
                ParentCategoryId = 3,
                Slug = "non-fiction",
                SortOrder = 2,
                IsActive = true
            },
            
            // Deeper nesting
            new Category { 
                Id = 10, 
                Name = "Android", 
                Description = "Android smartphones",
                ParentCategoryId = 4,
                Slug = "android",
                SortOrder = 1,
                IsActive = true
            },
            new Category { 
                Id = 11, 
                Name = "iOS", 
                Description = "Apple iOS smartphones",
                ParentCategoryId = 4,
                Slug = "ios",
                SortOrder = 2,
                IsActive = true
            }
        );
    }
}