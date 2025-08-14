

using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IContactInfoService
    {
        Task<ContactInfoViewDto> AddOrUpdateContactInfoAsync(ContactInfoCreateDto dto);
        Task<ContactInfoViewDto?> GetAllContactInfoAsync();
        Task DeleteContactInfoAsync(Guid id);
    }
}
