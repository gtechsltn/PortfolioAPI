

using System.Globalization;
using System.Text.Json.Serialization;

namespace Portfolio.Application.DTOs
{
    public class SkillSectionViewDto
    {
        public Guid Id { get; set; }
        public string SkillSectionTitle { get; set; }
        public string SkillSectionDescription { get; set; }
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        public string? DisplayCreatedAt => CreatedAt?.ToString("yyyy-MM-dd HH:mm tt", CultureInfo.InvariantCulture);
        public string? UpdatedBy { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        public string? DisplayUpdatedAt => UpdatedAt?.ToString("yyyy-MM-dd HH:mm tt", CultureInfo.InvariantCulture);
    }
}
