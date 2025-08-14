

namespace Portfolio.Domain.Entities
{
    public class ClientMessage
    {
        public Guid Id { get; set; }
        public string? ClientName { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientSubject { get; set; } 
        public string? ClientMessageContent { get; set; } 
        public DateTime? SentMessageAt { get; set; }

        public static ClientMessage Create(
            string? clientName,
            string? clientEmail,
            string? clientSubject,
            string? clientMessageContent)
        {
            return new ClientMessage
            {
                ClientName = clientName,
                ClientEmail = clientEmail,
                ClientSubject = clientSubject,
                ClientMessageContent = clientMessageContent,
                SentMessageAt = DateTime.UtcNow
            };
        }

        public void Update(
            string? clientName = null,
            string? clientEmail = null,
            string? clientSubject = null,
            string? clientMessageContent = null)
        {
            if (!string.IsNullOrWhiteSpace(clientName)) ClientName = clientName;
            if (!string.IsNullOrWhiteSpace(clientEmail)) ClientEmail = clientEmail;
            if (!string.IsNullOrWhiteSpace(clientSubject)) ClientSubject = clientSubject;
            if (!string.IsNullOrWhiteSpace(clientMessageContent)) ClientMessageContent = clientMessageContent;
            SentMessageAt = DateTime.UtcNow;
        }
    }
}
