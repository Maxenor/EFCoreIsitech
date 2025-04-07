using System.ComponentModel.DataAnnotations;

namespace EFCoreIsitech.Data.Models;

public class ProductInventory
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    [Required]
    public int QuantityInStock { get; set; }
    
    public int? ReorderThreshold { get; set; }
    
    [MaxLength(50)]
    public string? SKU { get; set; }
    
    [MaxLength(100)]
    public string? Location { get; set; }
    
    public DateTime LastRestockDate { get; set; } = DateTime.UtcNow;
    
    public DateTime LastCountDate { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public virtual Product Product { get; set; } = null!;
}