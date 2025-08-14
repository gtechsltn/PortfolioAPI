

using Microsoft.AspNetCore.Http;

namespace Portfolio.Application.DTOs
{
    public class IntroCreateDto
    {
        public string IntroName { get; set; }
        public string ProfessionalTitle { get; set; }
        public IFormFile IntroImage { get; set; }
        public IFormFile UserResume { get; set; }
    }
}
