using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreIsitech.Data.Models;

public class ProductPriceHistory
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    [Required]
    public DateTime EffectiveDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    [MaxLength(255)]
    public string? Reason { get; set; }
    
    [MaxLength(50)]
    public string? ChangedBy { get; set; }
    
    // Navigation property
    public virtual Product Product { get; set; } = null!;
}