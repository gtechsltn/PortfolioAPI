
using System.Globalization;
using System.Text.Json.Serialization;

namespace Portfolio.Application.DTOs
{
    public class IntroViewDto
    {
        public Guid Id { get; set; }
        public string IntroName { get; set; }
        public string ProfessionalTitle { get; set; }
        public string IntroImagePath { get; set; }
        public string ResumePath { get; set; }
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
