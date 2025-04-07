using System.ComponentModel.DataAnnotations;

namespace EFCoreIsitech.Data.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}