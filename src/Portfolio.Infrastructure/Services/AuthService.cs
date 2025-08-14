using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using System.Net;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

namespace Portfolio.Infrastructure.Service
{

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthService> _logger;
        private const string Admin = "Admin";
        public AuthService(
            IUnitOfWork unitofWork,
            IJwtTokenGenerator tokenService,
            IEmailSender emailSender,
            ILogger<AuthService> logger)
        {
            _unitOfWork = unitofWork;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _logger = logger;
            _passwordHasher = new PasswordHasher<User>();
        }

        //---------------------------------------------------------------- Global Exception Handling Middleware-------------------------------------
        #region register
        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {

            var existingUser = (await _unitOfWork.UserRepository.FindAsync(u => u.Email == request.Email)).FirstOrDefault();
            if (existingUser != null)
                throw new AlreadyExistsException("User with this email already exists.");

            if (request.Role == Admin)
            {
                var adminExists = (await _unitOfWork.UserRepository.FindAsync(u => u.Role == Admin)).Any();
                if (adminExists)
                    throw new AlreadyExistsException("An Admin user already exists. Only one Admin is allowed.");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Role = request.Role
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User {Email} registered successfully with role {Role}.", user.Email, user.Role);

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Role = user.Role,
                Token = token,
                RefreshToken = user.RefreshToken
            };

        }
        #endregion

        #region login
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = (await _unitOfWork.UserRepository.FindAsync(u => u.Email == request.Email)).FirstOrDefault();
            if (user == null)
                throw new NotFoundException("User not found.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new InvalidCredentialsException("Invalid credentials.");

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Role = user.Role,
                Token = token,
                RefreshToken = user.RefreshToken
            };
        }
        #endregion

        #region refresh token
        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var users = await _unitOfWork.UserRepository.FindAsync(u => u.RefreshToken == request.RefreshToken);
            var user = users.FirstOrDefault();

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new InvalidOrExpiredRefreshTokenException("Invalid or expired refresh token");

            var newJwt = _tokenService.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new RefreshTokenResponseDto
            {
                Token = newJwt,
                RefreshToken = newRefreshToken
            };
        }
        #endregion

        #region generate refresh token
        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
        #endregion

        #region send reset password token
        public async Task SendResetPasswordTokenAsync(ForgotPasswordRequestDto request)
        {
            var user = (await _unitOfWork.UserRepository.FindAsync(u => u.Email == request.Email)).FirstOrDefault()
                ?? throw new NotFoundException("User not found.");

            var range = RandomNumberGenerator.GetInt32(100000, 999999);
            var otp = range.ToString();

            user.PasswordResetToken = otp;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(10);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            string htmlBody = GenerateResetPasswordEmailBody(user, otp);
            await _emailSender.SendEmailAsync(user.Email, "Password Reset OTP", htmlBody);
        }
        #endregion

        #region reset password
        public async Task ResetUserPasswordAsync(ResetPasswordDto request)
        {
            var user = (await _unitOfWork.UserRepository.FindAsync(u =>
                u.PasswordResetToken == request.OTP &&
                u.PasswordResetTokenExpiry > DateTime.UtcNow)).FirstOrDefault();

            if (user == null)
                throw new NotFoundException("Invalid or expired OTP.");

            user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region generate reset password email body
        private string GenerateResetPasswordEmailBody(User user, string otp)
        {
            string htmlBody = $@"
            <html>
              <body style='font-family: Arial, sans-serif; color: #333;'>
                <h2>Password Reset OTP</h2>
                <p>Hello {user.FullName},</p>
                <p>You requested to reset your password. Use the OTP below:</p>
                <h2 style='color:#007BFF'>{otp}</h2>
                <p>This OTP will expire in 10 minutes. Please do not share it.</p>
                <p>If you didn’t request this, ignore this email.</p>
                <br/>
                <p>Regards,<br/>Your Company Team</p>
              </body>
            </html>";
            return htmlBody;
        }
        #endregion

    }
}
