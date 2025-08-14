
using System.Globalization;
using System.Text.Json.Serialization;

namespace Portfolio.Application.DTOs
{
    public class HeaderViewDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string LogoPath { get; set; }
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
