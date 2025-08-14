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
    public class EducationService : IEducationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;
        public EducationService(
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

        #region add education
        public async Task<EducationViewDto> AddEducationAsync(EducationCreateDto dto)
        {
            _mapper.Map<Education>(dto);
            var education = Education.Create(
                qualification: dto.Qualification,
                startDate: dto.StartDate,
                endDate: dto.IsCurrentlyStudying ? null : dto.EndDate,
                isCurrentlyStudying: dto.IsCurrentlyStudying,
                instituteName: dto.InstituteName,
                educationDetail: dto.EducationDetail
            );
            await _unitOfWork.EducationRepository.AddAsync(education);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EducationViewDto>(education);
        }
        #endregion

        #region update education
        public async Task<EducationViewDto> UpdateEducationAsync(Guid id, EducationCreateDto dto)
        {
            var education = await _unitOfWork.EducationRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Experience not found.");

            education.Update(
                qualification: dto.Qualification,
                startDate: dto.StartDate,
                endDate: dto.IsCurrentlyStudying ? null : dto.EndDate,
                isCurrentlyStudying: dto.IsCurrentlyStudying,
                instituteName: dto.InstituteName,
                educationDetail: dto.EducationDetail
            );
            await _unitOfWork.EducationRepository.UpdateAsync(education);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EducationViewDto>(education);

        }
        #endregion

        #region get all education
        public async Task<IEnumerable<EducationViewDto>> GetAllEducationAsync()
        {
            var education = await _unitOfWork.EducationRepository.FindAsync(e => e.IsCurrentlyStudying || e.EndDate != null);

            return education == null ? null : _mapper.Map<IEnumerable<EducationViewDto>>(education);
        }
        #endregion

        #region delete education
        public async Task DeleteEducationAsync(Guid id)
        {
            var education = await _unitOfWork.EducationRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Education not found.");

            var currentUserName = _currentUserService.GetCurrentUserName();

            var auditLog = AuditLogHelper.CreateDeleteLog(education, "Delete", "Education", currentUserName);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.EducationRepository.DeleteAsync(education);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

    }
}
