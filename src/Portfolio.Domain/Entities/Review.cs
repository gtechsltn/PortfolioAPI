
using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Review : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string? ClientReview { get; set; }
        public string? ClientImagePath { get; set; }
        public string? ClientName { get; set; }
        public string? ClientProfession { get; set; }

        public static Review Create(
            string? clientReview,
            string? clientImagePath,
            string? clientName,
            string? clientProfession)
        {
            return new Review
            {
                ClientReview = clientReview,
                ClientImagePath = clientImagePath,
                ClientName = clientName,
                ClientProfession = clientProfession
            };
        }
        public void Update(
            string? clientReview = null,
            string? clientImagePath = null,
            string? clientName = null,
            string? clientProfession = null)
        {
            if (!string.IsNullOrWhiteSpace(clientReview)) ClientReview = clientReview;
            if (!string.IsNullOrWhiteSpace(clientImagePath)) ClientImagePath = clientImagePath;
            if (!string.IsNullOrWhiteSpace(clientName)) ClientName = clientName;
            if (!string.IsNullOrWhiteSpace(clientProfession)) ClientProfession = clientProfession;
        }
    }
}
