

using System.Text.Json.Serialization;

namespace Portfolio.Application.DTOs
{
    public class ClientMessageViewDto
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientSubject { get; set; }
        public string ClientMessageContent { get; set; }
        
        [JsonIgnore]
        public DateTime? SentMessageAtRaw { get; set; }
        public string? SentMessageAt => SentMessageAtRaw.HasValue
        ? TimeZoneInfo.ConvertTimeFromUtc(
              SentMessageAtRaw.Value,
              TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time"))
            .ToString("dd-MMM-yyyy hh:mm tt")
        : null;
    }
}
