

using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services
{
    public class SocialLinksService : ISocialLinksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;

        public SocialLinksService(
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

        #region add or update social links
        public async Task<SocialLinksViewDto> AddOrUpdateSocialLinksAsync(SocialLinksCreateDto dto)
        {
            var existingSocialLinks = (await _unitOfWork.SocialLinksRepository.GetAllAsync()).FirstOrDefault();

            SocialLinks socialLinks;

            if (existingSocialLinks != null)
            {
                _mapper.Map(dto, existingSocialLinks);

                existingSocialLinks.Update(
                    linkedIn: dto.LinkedIn,
                    twitter: dto.Twitter,
                    instagram: dto.Instagram,
                    facebook: dto.Facebook
                );
                socialLinks = existingSocialLinks;
            }
            else
            {
                var mappedSocialLinks = _mapper.Map<SocialLinks>(dto);
                socialLinks = SocialLinks.Create(
                    linkedIn: mappedSocialLinks.LinkedIn,
                    twitter: mappedSocialLinks.Twitter,
                    instagram: mappedSocialLinks.Instagram,
                    facebook: mappedSocialLinks.Facebook
                );

                await _unitOfWork.SocialLinksRepository.AddAsync(socialLinks);
            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SocialLinksViewDto>(socialLinks);
        }
        #endregion

        #region get all social links
        public async Task<SocialLinksViewDto> GetAllSocialLinksAsync()
        {
            var socialLinks = (await _unitOfWork.SocialLinksRepository.GetAllAsync()).FirstOrDefault();
            return socialLinks == null ? null : _mapper.Map<SocialLinksViewDto>(socialLinks);
        }
        #endregion

        #region delete social links
        public async Task DeleteSocialLinksAsync(Guid id)
        {
            var socialLinks = await _unitOfWork.SocialLinksRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Social links not found.");

            var currentUser = _currentUserService.GetCurrentUserName();
            var auditLog = AuditLogHelper.CreateDeleteLog(socialLinks, "SocialLinks", "Delete", currentUser);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.SocialLinksRepository.DeleteAsync(socialLinks);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

    }
}
