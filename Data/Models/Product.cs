using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreIsitech.Data.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    
    // Navigation properties
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    // New navigation properties for our added features
    public virtual ProductInventory? Inventory { get; set; }
    
    public virtual ICollection<ProductRating> Ratings { get; set; } = new List<ProductRating>();
    
    public virtual ICollection<ProductPriceHistory> PriceHistory { get; set; } = new List<ProductPriceHistory>();
    
    // Computed properties
    [NotMapped]
    public decimal AverageRating => (decimal)(Ratings.Any() ? Ratings.Average(r => r.Rating) : 0);
    
    [NotMapped]
    public int ReviewCount => Ratings.Count;
    
    [NotMapped]
    public bool InStock => Inventory != null && Inventory.QuantityInStock > 0;
}