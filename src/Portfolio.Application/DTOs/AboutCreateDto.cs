using Microsoft.AspNetCore.Http;

namespace Portfolio.Application.DTOs
{
    public class AboutCreateDto
    {
        public IFormFile? AboutImage { get; set; }
        public string? Name { get; set; }
        public string? Profession { get; set; }
        public string? Description { get; set; }
        public DateOnly? Birthday { get; set; }
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Languages { get; set; }
        public string? FreelanceStatus { get; set; }
    }
}
