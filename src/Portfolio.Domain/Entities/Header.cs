using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Header : AddUpdateAuditEntities
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LogoPath { get; set; }
        public string PhoneNumber { get; set; }
       

        public static Header Create(
            string phoneNumber, 
            string logoPath)
        {
            return new Header
            {
                PhoneNumber = phoneNumber,
                LogoPath = logoPath
            };
        }

        public void Update(
            string phoneNumber,
            string logoPath)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber)) PhoneNumber = phoneNumber;
            if (!string.IsNullOrWhiteSpace(logoPath)) LogoPath = logoPath;
            
        }

        // Optional: Keep these if needed individually elsewhere
        public void SetPhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;
        public void SetLogoPath(string logoPath) => LogoPath = logoPath;
    }
}
