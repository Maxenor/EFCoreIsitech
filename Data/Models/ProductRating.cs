using System.ComponentModel.DataAnnotations;

namespace EFCoreIsitech.Data.Models;

public class ProductRating
{
    public int Id { get; set; }
    
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
    
    [MaxLength(500)]
    public string? Review { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Foreign keys
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    
    // Navigation properties
    public virtual Product Product { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    
    // Additional properties
    public bool IsVerifiedPurchase { get; set; }
    public int? HelpfulVotes { get; set; }
}