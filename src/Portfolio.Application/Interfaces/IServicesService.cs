
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IServicesService
    {
        Task<ServicesViewDto> AddServicesAsync(ServicesCreateDto dto);
        Task<IEnumerable<ServicesViewDto>> GetAllServicesAsync();
        Task<ServicesViewDto> UpdateServiceAsync(Guid id, ServicesCreateDto dto);
        Task DeleteServiceAsync(Guid id);
    }
}
