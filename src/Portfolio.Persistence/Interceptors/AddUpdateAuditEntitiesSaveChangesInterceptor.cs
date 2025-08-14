
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Common;
using System.Security.Claims;

namespace Portfolio.Persistence.Interceptors
{
    public class AddUpdateAuditEntitiesSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        public AddUpdateAuditEntitiesSaveChangesInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync( 
            DbContextEventData eventDate, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            var context = eventDate.Context;

            if (context == null)
            {
                return base.SavingChangesAsync(eventDate, result, cancellationToken);
            }

            var user = _currentUserService.GetCurrentUserName();

            foreach (var entry in context.ChangeTracker.Entries<AddUpdateAuditEntities>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = user;
                    entry.Entity.CreatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedBy = user;
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }
            return base.SavingChangesAsync(eventDate, result, cancellationToken);
        }
    }
}
