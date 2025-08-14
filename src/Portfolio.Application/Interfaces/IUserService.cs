

using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(string currentUserRole);
        Task<UserResponseDto> GetUserByIdAsync(Guid id);
        Task<UserResponseDto> UpdateUserAsync(Guid id, UpdateUserRequestDto request, string currentUserRole, Guid currentUserId);
        Task DeleteUserAsync(Guid id, string currentUserRole, Guid currentUserId);
        Task ChangePasswordAsync(ChangePasswordRequestDto request, Guid currentUserId, string currentUserRole);


    }
}
