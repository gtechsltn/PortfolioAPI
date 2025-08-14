
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface ISkillDetailService
    {
        Task<SkillDetailViewDto> AddSkillDetailAsync(SkillDetailCreateDto dto);
        Task<SkillDetailViewDto> UpdateSkillDetailAsync(Guid id, SkillDetailCreateDto dto);
        Task DeleteSkillDetailAsync(Guid id);
        Task<IEnumerable<SkillDetailViewDto>> GetAllSkillDetailAsync();

    }
}
