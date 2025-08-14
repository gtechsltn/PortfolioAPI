
namespace Portfolio.Application.DTOs
{
    public class AuthResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
    }
}
