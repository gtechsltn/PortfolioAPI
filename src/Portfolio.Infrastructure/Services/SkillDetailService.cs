using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services
{
    public class SkillDetailService : ISkillDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;

        public SkillDetailService(
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

        #region add skill detail
        public async Task<SkillDetailViewDto> AddSkillDetailAsync(SkillDetailCreateDto dto)
        {
            var existingCount = (await _unitOfWork.SkillDetailRepository.GetAllAsync()).Count();

            SkillDetail skillDetail;
            if (existingCount >= 6)
            {
                throw new InvalidOperationException("You can only add up to 6 skills detail.");
            }
            else
            {
                _mapper.Map<SkillDetail>(dto);

                skillDetail = SkillDetail.Create(
                    skillName: dto.SkillName,
                    proficiency: dto.Proficiency
                );
                await _unitOfWork.SkillDetailRepository.AddAsync(skillDetail);
            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SkillDetailViewDto>(skillDetail);
        }
        #endregion

        #region update skill detail
        public async Task<SkillDetailViewDto> UpdateSkillDetailAsync(Guid id, SkillDetailCreateDto dto)
        {
            var skillDetail = await _unitOfWork.SkillDetailRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Skill detail not found");

            skillDetail.Update(
                skillName: dto.SkillName,
                proficiency: dto.Proficiency
            );
            await _unitOfWork.SkillDetailRepository.UpdateAsync(skillDetail);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SkillDetailViewDto>(skillDetail);
        }
        #endregion

        #region get skill detail
        public async Task<IEnumerable<SkillDetailViewDto>> GetAllSkillDetailAsync()
        {
            var skillDetails = await _unitOfWork.SkillDetailRepository.GetAllAsync()
                ?? throw new NotFoundException("No skill details found.");

            return _mapper.Map<IEnumerable<SkillDetailViewDto>>(skillDetails);
        }
        #endregion

        #region delete skill detail
        public async Task DeleteSkillDetailAsync(Guid id)
        {
            var skillDetail = await _unitOfWork.SkillDetailRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Skill detail not found.");

            var currentUser = _currentUserService.GetCurrentUserName();
            
            var auditLog = AuditLogHelper.CreateDeleteLog(skillDetail, "Delete", "SkillDetail", currentUser);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.SkillDetailRepository.DeleteAsync(skillDetail);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

    }
}
