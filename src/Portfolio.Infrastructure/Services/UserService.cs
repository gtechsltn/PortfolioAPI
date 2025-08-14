using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;
        private const string AdminRole = "Admin";

        public UserService(
            IUnitOfWork unitOfWork,
            IJwtTokenGenerator tokenGenerator,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            ICurrentUserService currentUserService,
            IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _currentUserService = currentUserService;
            _auditLogService = auditLogService;
        }

        #region update user
        public async Task<UserResponseDto> UpdateUserAsync(Guid id, UpdateUserRequestDto request, string currentUserRole, Guid currentUserId)
        {
            var isSelf = currentUserId == id;
            var isAdmin = currentUserRole == AdminRole;

            if (!isSelf && !isAdmin)
                throw new UnauthorizedAccessException("You are not authorized to update this account.");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("User not found.");

            user.Update(
                fullName: request.FullName ?? user.FullName,
                email: request.Email ?? user.Email
            );

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(user);
        }
        #endregion

        #region change user password
        public async Task ChangePasswordAsync(ChangePasswordRequestDto request, Guid currentUserId, string currentUserRole)
        {
            var isAdmin = currentUserRole == AdminRole;
            var user = await GetTargetUserAsync(request.Email, currentUserId, isAdmin);

            if (user.Id == currentUserId)
            {
                ValidateCurrentPassword(user, request.CurrentPassword);
            }
            else if (!isAdmin)
            {
                throw new UnauthorizedAccessException("You are not authorized to change this password.");
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region get all users
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(string currentUserRole)
        {
            if (currentUserRole != AdminRole)
                throw new UnauthorizedAccessException("Only admin can view all users.");

            var users = await _unitOfWork.UserRepository.GetAllAsync()
                ?? throw new NotFoundException("No users found.");

            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }
        #endregion

        #region get user by id
        public async Task<UserResponseDto> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("User not found.");
            return _mapper.Map<UserResponseDto>(user);
        }
        #endregion

        #region delete user
        public async Task DeleteUserAsync(Guid id, string currentUserRole, Guid currentUserId)
        {
            if (currentUserRole != AdminRole)
                throw new UnauthorizedAccessException("Only admin can delete users.");

            if (currentUserId == id)
                throw new InvalidOperationException("You cannot delete your own account.");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("User not found.");

            var currentUser = _currentUserService.GetCurrentUserName();

            var auditLog = AuditLogHelper.CreateDeleteLog(user, "Delete", "User", currentUser);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.UserRepository.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Helper methods
        private async Task<User> GetTargetUserAsync(string? email, Guid currentUserId, bool isAdmin)
        {
            if (isAdmin && !string.IsNullOrWhiteSpace(email))
            {
                return (await _unitOfWork.UserRepository.FindAsync(u => u.Email == email)).FirstOrDefault()
                    ?? throw new NotFoundException("User not found by email.");
            }

            return await _unitOfWork.UserRepository.GetByIdAsync(currentUserId)
                ?? throw new NotFoundException("User not found.");
        }

        private void ValidateCurrentPassword(User user, string? currentPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new InvalidOperationException("Current password is required.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);
            if (result == PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Current password is incorrect.");
        }
        #endregion

    }
}
