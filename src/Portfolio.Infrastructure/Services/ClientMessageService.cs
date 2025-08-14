using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services
{
    public class ClientMessageService : IClientMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ClientMessageService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        #region add client message
        public async Task<ClientMessageViewDto> AddClientMessageAsync(ClientMessageCreateDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto), "Client message data cannot be null.");
            }
            var clientMessage = ClientMessage.Create(
                clientName: dto.ClientName,
                clientEmail: dto.ClientEmail,
                clientSubject: dto.ClientSubject,
                clientMessageContent: dto.ClientMessageContent
            );
            await _unitOfWork.ClientMessageRepository.AddAsync(clientMessage);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ClientMessageViewDto>(clientMessage);
        }
        #endregion

        #region update client message
        public async Task<ClientMessageViewDto> UpdateClientMessageAsync(Guid id, ClientMessageCreateDto dto)
        {
            var clientMessage = await _unitOfWork.ClientMessageRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Client message not found.");

            clientMessage.Update(
                clientName: dto.ClientName,
                clientEmail: dto.ClientEmail,
                clientSubject: dto.ClientSubject,
                clientMessageContent: dto.ClientMessageContent
            );
            await _unitOfWork.ClientMessageRepository.UpdateAsync(clientMessage);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ClientMessageViewDto>(clientMessage);
        }
        #endregion

        #region get all client message
        public async Task<IEnumerable<ClientMessageViewDto>> GetAllClientMessageAsync()
        {
            var clientMessages = await _unitOfWork.ClientMessageRepository.GetAllAsync()
                ?? throw new NotFoundException("No client messages found.");

            return _mapper.Map<IEnumerable<ClientMessageViewDto>>(clientMessages);
        }
        #endregion

        #region get client message by id
        public async Task<ClientMessageViewDto> GetClientMessageByIdAsync(Guid id)
        {
            var clientMessage = await _unitOfWork.ClientMessageRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Client message not found.");

            return _mapper.Map<ClientMessageViewDto>(clientMessage);
        }
        #endregion

        #region delete client message
        public async Task DeleteClientMessageAsync(Guid id)
        {
            var clientMessage = await _unitOfWork.ClientMessageRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Client message not found.");

            var currentUser = _currentUserService.GetCurrentUserName();

            var auditLog = AuditLogHelper.CreateDeleteLog(clientMessage, "Delete", "ClientMessage", currentUser);
            await _unitOfWork.AuditLogRepository.AddAsync(auditLog);

            await _unitOfWork.ClientMessageRepository.DeleteAsync(clientMessage);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

    }
}
