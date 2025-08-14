
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task AddAuditLogAsync(AuditLog log);
    }
}
