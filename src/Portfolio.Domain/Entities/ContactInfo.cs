

using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class ContactInfo : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string? ContactInfoDetail { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public static ContactInfo Create(
            string? contactInfoDetail,
            string? email,
            string? phoneNumber,
            string? address)

        {
            return new ContactInfo
            {
                ContactInfoDetail = contactInfoDetail,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address
            };
            
        }

        public void Update(
            string? contacInfoDetail = null,
            string? email = null,
            string? phoneNumber = null,
            string? address = null)
        {
            if (!string.IsNullOrWhiteSpace(contacInfoDetail)) ContactInfoDetail = contacInfoDetail;
            if (!string.IsNullOrWhiteSpace(email)) Email = email;
            if (!string.IsNullOrWhiteSpace(phoneNumber)) PhoneNumber = phoneNumber;
            if (!string.IsNullOrWhiteSpace(address)) Address = address;
        }
    }
}
