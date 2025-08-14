using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task SendResetPasswordTokenAsync(ForgotPasswordRequestDto request);
        Task ResetUserPasswordAsync(ResetPasswordDto request);
    }
}
