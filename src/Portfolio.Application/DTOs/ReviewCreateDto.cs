

using Microsoft.AspNetCore.Http;

namespace Portfolio.Application.DTOs
{
    public class ReviewCreateDto
    {

        public string ClientReview { get; set; }
        public IFormFile? ClientImage { get; set; }
        public string ClientName { get; set; }
        public string ClientProfession { get; set; }
    }
}
