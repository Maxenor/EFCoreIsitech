using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreIsitech.Data.Models;

public abstract class AuditableEntity : IAuditable
{
    [MaxLength(50)]
    public string? CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    [MaxLength(50)]
    public string? LastModifiedBy { get; set; }
    
    public DateTime? LastModifiedAt { get; set; }

    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}