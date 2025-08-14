

using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface ISkillSectionService
    {
        Task<SkillSectionViewDto> AddOrUpdateSkillSectionAsync(SkillSectionCreateDto dto);
        Task <SkillSectionViewDto?> GetSkillSectionAsync();
        Task DeleteSkillSectionAsync(Guid id);
    }
}
