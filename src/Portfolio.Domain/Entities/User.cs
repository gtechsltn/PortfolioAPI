

using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class User : AddUpdateAuditEntities
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }


        public void Update(string fullName, string email)
        {
            FullName = fullName;
            Email = email;

            if (!string.IsNullOrEmpty(fullName)) FullName = fullName;
            if (!string.IsNullOrEmpty(email)) Email = email;


        }
    }
}
