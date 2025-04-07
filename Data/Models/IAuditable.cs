using System;

namespace EFCoreIsitech.Data.Models;
public interface IAuditable
{
    string? CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string? LastModifiedBy { get; set; }
    DateTime? LastModifiedAt { get; set; }
    string? DeletedBy { get; set; }
    DateTime? DeletedAt { get; set; }
    bool IsDeleted { get; set; }
}