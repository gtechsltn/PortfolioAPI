
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IClientMessageService
    {
        Task<ClientMessageViewDto> AddClientMessageAsync(ClientMessageCreateDto dto);
        Task<ClientMessageViewDto> GetClientMessageByIdAsync(Guid id);
        Task<IEnumerable<ClientMessageViewDto>> GetAllClientMessageAsync();
        Task<ClientMessageViewDto> UpdateClientMessageAsync(Guid id, ClientMessageCreateDto dto);
        Task DeleteClientMessageAsync(Guid id);
    }
}
