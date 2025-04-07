using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreIsitech.Data.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public int? ParentCategoryId { get; set; }
    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
    
    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    
    // Additional properties for categorization
    public string? IconUrl { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    
    [MaxLength(100)]
    public string? Slug { get; set; }
    
    // Computed property to get the full path (e.g., "Electronics > Phones > Smartphones")
    [NotMapped]
    public string FullPath 
    { 
        get
        {
            if (ParentCategory == null)
                return Name;
            
            return $"{ParentCategory.FullPath} > {Name}";
        }
    }
}