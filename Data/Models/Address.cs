using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreIsitech.Data.Models;

public class Address
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Street { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string City { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Country { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string? State { get; set; }
    
    public AddressType Type { get; set; }
    
    public int CustomerId { get; set; }
    
    // Navigation property
    public virtual Customer Customer { get; set; } = null!;
    
    [MaxLength(200)]
    public string? AdditionalInfo { get; set; }
    
    public bool IsDefault { get; set; }
}

public enum AddressType
{
    Billing,
    Shipping
}