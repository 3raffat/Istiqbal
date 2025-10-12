using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;


namespace Istiqbal.Infrastructure.Data.Interceptors
{
    public sealed class AuditableEntityInterceptor(IUser user, TimeProvider timeProvider) : SaveChangesInterceptor
    {
        private readonly IUser _user = user;
        private readonly TimeProvider _timeProvider = timeProvider;
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public void UpdateEntities(DbContext? context)
        {
            if(context is null) return;

            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
            {
                if(entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    var utcNow = _timeProvider.GetUtcNow();

                    if (entry.State is EntityState.Added)
                    {
                        entry.Entity.CreatedBy = _user.Id;
                        entry.Entity.CreatedAtUtc = utcNow;
                    }

                    entry.Entity.LastModifiedBy = _user.Id;
                    entry.Entity.LastModifiedUtc = utcNow;

                    foreach (var ownedEntry in entry.References)
                    {
                        if (ownedEntry.TargetEntry is { Entity: AuditableEntity ownedEntity } && ownedEntry.TargetEntry.State is EntityState.Added or EntityState.Modified)
                        {
                            if (ownedEntry.TargetEntry.State == EntityState.Added)
                            {
                                ownedEntity.CreatedBy = _user.Id;
                                ownedEntity.CreatedAtUtc = utcNow;
                            }

                            ownedEntity.LastModifiedBy = _user.Id;
                            ownedEntity.LastModifiedUtc = utcNow;
                        }
                    }

                    if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedUtc = utcNow;
                        entry.Entity.DeletedBy = _user.Id;
                    }
                }
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry?.Metadata.IsOwned() == true &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}