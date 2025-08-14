

using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface ISocialLinksService
    {
        Task<SocialLinksViewDto> AddOrUpdateSocialLinksAsync(SocialLinksCreateDto dto);
        Task<SocialLinksViewDto> GetAllSocialLinksAsync();
        Task DeleteSocialLinksAsync(Guid id);
    }
}
