namespace Portfolio.Domain.Entities
{
    public class Header
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LogoPath { get; set; }
        public string PhoneNumber { get; set; }

        public static Header Create(string phoneNumber, string logoPath)
        {
            return new Header
            {
                PhoneNumber = phoneNumber,
                LogoPath = logoPath
            };
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetLogoPath(string logoPath)
        {
            LogoPath = logoPath;
        }
    }
}
