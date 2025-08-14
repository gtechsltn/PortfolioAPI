

using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IExperienceService
    {
        Task<ExperienceViewDto> AddExperienceAsync(ExperienceCreateDto dto);
        Task<ExperienceViewDto> UpdateExperienceAsync(Guid id, ExperienceCreateDto dto);
        Task<IEnumerable<ExperienceViewDto>> GetAllExperienceAsync();
        Task DeleteExperienceAsync(Guid id);
    }
}
