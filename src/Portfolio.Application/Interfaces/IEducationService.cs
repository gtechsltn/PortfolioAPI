
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IEducationService
    {
        Task<EducationViewDto> AddEducationAsync(EducationCreateDto dto);
        Task<EducationViewDto> UpdateEducationAsync(Guid id, EducationCreateDto dto);
        Task<IEnumerable<EducationViewDto>> GetAllEducationAsync();
        Task DeleteEducationAsync(Guid id);
    }
}
