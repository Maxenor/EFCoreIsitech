using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreIsitech.Data.Models;

public class Customer
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? Phone { get; set; }
    
    // Navigation properties
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    // New navigation property for addresses
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    
    [NotMapped]
    public Address? DefaultBillingAddress => Addresses.FirstOrDefault(a => a.Type == AddressType.Billing && a.IsDefault);
    
    [NotMapped]
    public Address? DefaultShippingAddress => Addresses.FirstOrDefault(a => a.Type == AddressType.Shipping && a.IsDefault);
}