using EFCoreIsitech.Data.Models;
using EFCoreIsitech.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreIsitech.Data.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditFields(DbContext? context)
    {
        if (context == null) return;

        var now = DateTime.UtcNow;
        var currentUser = _currentUserService.GetCurrentUsername() ?? "System";

        var auditableEntities = context.ChangeTracker.Entries<IAuditable>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in auditableEntities)
        {
            if (entry.State == EntityState.Added)
            {
                // Set creation fields for new entities
                entry.Entity.CreatedAt = now;
                entry.Entity.CreatedBy = currentUser;
                entry.Entity.LastModifiedAt = now;
                entry.Entity.LastModifiedBy = currentUser;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(e => e.CreatedAt).IsModified = false;
                entry.Property(e => e.CreatedBy).IsModified = false;

                entry.Entity.LastModifiedAt = now;
                entry.Entity.LastModifiedBy = currentUser;
            }
        }
    }
}