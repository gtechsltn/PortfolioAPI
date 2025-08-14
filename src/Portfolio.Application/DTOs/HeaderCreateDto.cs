using Microsoft.AspNetCore.Http;

namespace Portfolio.Application.DTOs
{
    public class HeaderCreateDto
    {
        public string PhoneNumber { get; set; }
        public IFormFile LogoImage { get; set; }
    }
}
