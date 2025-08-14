
using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class About : AddUpdateAuditEntities
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public string? Name { get; set; }
        public string? Profession { get; set; }
        public string? Description { get; set; }
        public DateOnly? Birthday { get; set; }
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Languages { get; set; }
        public string? FreelanceStatus { get; set; }

        public static About Create(
            string? imagePath,
            string? name,
            string? profession,
            string? description,
            DateOnly? birthday,
            string? location,
            string? phoneNumber,
            string? email,
            string? languages,
            string? freelanceStatus)
        {
            return new About
            {
                ImagePath = imagePath,
                Name = name,
                Profession = profession,
                Description = description,
                Birthday = birthday,
                Location = location,
                PhoneNumber = phoneNumber,
                Email = email,
                Languages = languages,
                FreelanceStatus = freelanceStatus
            };
        }
        public void Update(
            string? imagePath = null,
            string? name = null,
            string? profession = null,
            string? description = null,
            DateOnly? birthday = null,
            string? location = null,
            string? phoneNumber = null,
            string? email = null,
            string? languages = null,
            string? freelanceStatus = null)
        {
            if (!string.IsNullOrWhiteSpace(imagePath)) ImagePath = imagePath;
            if (!string.IsNullOrWhiteSpace(name)) Name = name;
            if (!string.IsNullOrWhiteSpace(profession)) Profession = profession;
            if (!string.IsNullOrWhiteSpace(description)) Description = description;
            if (birthday.HasValue) Birthday = birthday;
            if (!string.IsNullOrWhiteSpace(location)) Location = location;
            if (!string.IsNullOrWhiteSpace(phoneNumber)) PhoneNumber = phoneNumber;
            if (!string.IsNullOrWhiteSpace(email)) Email = email;
            if (!string.IsNullOrWhiteSpace(languages)) Languages = languages;
            if (!string.IsNullOrWhiteSpace(freelanceStatus)) FreelanceStatus = freelanceStatus;
        }

    }
}
