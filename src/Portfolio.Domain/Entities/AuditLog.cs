

namespace Portfolio.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EntityName { get; set; } = "";
        public string Action { get; set; } = "Unknown";
        public string? EntityId { get; set; }
        public string? PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
        public string? Details { get; set; }
    }

}
