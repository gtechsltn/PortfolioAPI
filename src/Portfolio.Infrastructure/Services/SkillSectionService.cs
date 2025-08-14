using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services
{
    public class SkillSectionService : ISkillSectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;

        public SkillSectionService(
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

        #region add or update skill section
        public async Task<SkillSectionViewDto> AddOrUpdateSkillSectionAsync(SkillSectionCreateDto dto)
        {
            var existingSkillSection = (await _unitOfWork.SkillSectionRepository.GetAllAsync()).FirstOrDefault();

            SkillSection skillSection;

            if (existingSkillSection != null)
            {
                _mapper.Map(dto, existingSkillSection);

                existingSkillSection.Update(
                    skillSectionTitle: dto.SkillSectionTitle,
                    skillSectionDescription: dto.SkillSectionDescription);
                skillSection = existingSkillSection;
            }
            else
            {
                var mappedSkill = _mapper.Map<SkillSection>(dto);

                skillSection = SkillSection.Create(
                skillSectionTitle: mappedSkill.SkillSectionTitle,
                skillSectionDescription: mappedSkill.SkillSectionDescription);
                await _unitOfWork.SkillSectionRepository.AddAsync(skillSection);
            }

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SkillSectionViewDto>(skillSection);
        }
        #endregion

        #region get skill section
        public async Task<SkillSectionViewDto?> GetSkillSectionAsync()
        {
            var skillSection = (await _unitOfWork.SkillSectionRepository.GetAllAsync()).FirstOrDefault()
                ?? throw new NotFoundException("Skill section not found.");

            return _mapper.Map<SkillSectionViewDto>(skillSection);
        }
        #endregion

        #region delete skill section
        public async Task DeleteSkillSectionAsync(Guid id)
        {
            var sekillSection = await _unitOfWork.SkillSectionRepository.GetByIdAsync(id);

            var currentUser = _currentUserService.GetCurrentUserName();

            var auditLog = AuditLogHelper.CreateDeleteLog(sekillSection, "Delete", "SkillSection", currentUser);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.SkillSectionRepository.DeleteAsync(sekillSection);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
