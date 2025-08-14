using AutoMapper;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using System.Security.Claims;

namespace Portfolio.Infrastructure.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;
        public ExperienceService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _auditLogService = auditLogService;
        }

        #region add experience
        public async Task<ExperienceViewDto> AddExperienceAsync(ExperienceCreateDto dto)
        {

            _mapper.Map<Experience>(dto);
            var experience = Experience.Create(
                companyName: dto.CompanyName,
                designation: dto.Designation,
                startDate: dto.StartDate,
                endDate: dto.IsCurrentlyWorking ? null : dto.EndDate,
                isCurrentlyWorking: dto.IsCurrentlyWorking,
                workDetail: dto.WorkDetail
            );
            await _unitOfWork.ExperienceRepository.AddAsync(experience);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ExperienceViewDto>(experience);
        }
        #endregion

        #region update experience
        public async Task<ExperienceViewDto> UpdateExperienceAsync(Guid id, ExperienceCreateDto dto)
        {
            var experience = await _unitOfWork.ExperienceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Experience not found.");

            experience.Update(
                companyName: dto.CompanyName,
                designation: dto.Designation,
                startDate: dto.StartDate,
                endDate: dto.IsCurrentlyWorking ? null : dto.EndDate,
                isCurrentlyWorking: dto.IsCurrentlyWorking,
                workDetail: dto.WorkDetail
            );
            await _unitOfWork.ExperienceRepository.UpdateAsync(experience);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ExperienceViewDto>(experience);

        }
        #endregion

        #region get all experience
        public async Task<IEnumerable<ExperienceViewDto>> GetAllExperienceAsync()
        {
            var experiences = await _unitOfWork.ExperienceRepository.FindAsync(e => e.IsCurrentlyWorking || e.EndDate != null);

            return experiences == null ? null : _mapper.Map<IEnumerable<ExperienceViewDto>>(experiences);
        }
        #endregion

        #region delete experience
        public async Task DeleteExperienceAsync(Guid id)
        {
            var experience = await _unitOfWork.ExperienceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Experience not found.");

            var currentUserName = _currentUserService.GetCurrentUserName();

            var auditLog = AuditLogHelper.CreateDeleteLog(experience, "Delete", "Experience", currentUserName);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.ExperienceRepository.DeleteAsync(experience);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        
    }
}
