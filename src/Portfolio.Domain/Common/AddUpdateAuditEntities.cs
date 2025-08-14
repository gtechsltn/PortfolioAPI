
using System.Globalization;
using System.Text.Json.Serialization;

namespace Portfolio.Domain.Common
{
    public class AddUpdateAuditEntities
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
