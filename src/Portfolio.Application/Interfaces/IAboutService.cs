using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IAboutService
    {
        Task<AboutViewDto> SaveOrUpdateAboutAsync(AboutCreateDto dto);
        Task<AboutViewDto?> GetAboutAsync();

    }
}
