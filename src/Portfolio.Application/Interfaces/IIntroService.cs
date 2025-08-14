using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IIntroService
    {
        Task<IntroViewDto> AddOrUpdateIntroAsync(IntroCreateDto dto);
        Task<IntroViewDto?> GetIntroAsync();
    }
}
