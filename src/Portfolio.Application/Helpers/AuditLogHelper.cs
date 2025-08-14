using Newtonsoft.Json;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Helpers
{
    public static class AuditLogHelper
    {
        public static AuditLog CreateDeleteLog<T>(T entity, string entityName, string action, string performedBy)
        {
            return new AuditLog
            {
                EntityName = entityName,
                Action = action,
                EntityId = entity?.GetType().GetProperty("Id")?.GetValue(entity)?.ToString(),
                PerformedBy = performedBy,
                PerformedAt = DateTime.Now,
                Details = JsonConvert.SerializeObject(entity)
            };
        }
    }
}
