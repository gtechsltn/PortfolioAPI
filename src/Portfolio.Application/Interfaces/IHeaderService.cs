using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IHeaderService
    {
        Task<HeaderViewDto> AddOrUpdateHeaderAsync(HeaderCreateDto dto);
        Task<HeaderViewDto?> GetHeaderAsync();
    }
}
