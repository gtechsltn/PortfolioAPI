
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuditLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAuditLogAsync(AuditLog log)
        {
            await _unitOfWork.AuditLogRepository.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
