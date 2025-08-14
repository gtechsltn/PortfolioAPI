using AutoMapper;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using System.Globalization;
using System.Security.Claims;

namespace Portfolio.Infrastructure.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;

        public ContactInfoService(
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


        #region add or update contact info
        public async Task<ContactInfoViewDto> AddOrUpdateContactInfoAsync(ContactInfoCreateDto dto)
        {
            var existingContactInfo = (await _unitOfWork.ContactInfoRepository.GetAllAsync()).FirstOrDefault();

            ContactInfo contactInfo;

            if (existingContactInfo != null)
            {
                _mapper.Map(dto, existingContactInfo);
                existingContactInfo.Update(
                    phoneNumber: dto.PhoneNumber,
                    email: dto.Email,
                    address: dto.Address
                );
                contactInfo = existingContactInfo;
            }
            else
            {
                var mappedContactInfo = _mapper.Map<ContactInfo>(dto);
                contactInfo = ContactInfo.Create(
                    contactInfoDetail: mappedContactInfo.ContactInfoDetail,
                    email: mappedContactInfo.Email,
                    phoneNumber: mappedContactInfo.PhoneNumber,
                    address: mappedContactInfo.Address
                );
                await _unitOfWork.ContactInfoRepository.AddAsync(contactInfo);

            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ContactInfoViewDto>(contactInfo);
        }

        #endregion

        #region get all contact info
        public async Task<ContactInfoViewDto?> GetAllContactInfoAsync()
        {
            var contactInfo = (await _unitOfWork.ContactInfoRepository.GetAllAsync()).FirstOrDefault();

           return contactInfo == null ? null : _mapper.Map<ContactInfoViewDto>(contactInfo);
        }
        #endregion

        #region delete contact info
        public async Task DeleteContactInfoAsync(Guid id)
        {
            var contactInfo = await _unitOfWork.ContactInfoRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Contact info not found.");

            var currentUserName = _currentUserService.GetCurrentUserName();

            var auditLog = AuditLogHelper.CreateDeleteLog(contactInfo, "Delete", "ContactInfo", currentUserName);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.ContactInfoRepository.DeleteAsync(contactInfo);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

    }
}
