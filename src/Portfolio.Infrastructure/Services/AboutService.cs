using AutoMapper;
using DotNetOpenAuth.InfoCard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using System.Globalization;
using System.Security.Claims;

namespace Portfolio.Infrastructure.Service
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;
        private const string AboutFolder = "AboutImages";
        public AboutService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment env,
            IFileStorageService fileStorageService,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserService currentUserService,
            IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _fileStorageService = fileStorageService;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
            _auditLogService = auditLogService;
        }

        #region save or update about
        public async Task<AboutViewDto> SaveOrUpdateAboutAsync(AboutCreateDto dto)
        {
            var existingAbout = (await _unitOfWork.AboutRepository.GetAllAsync()).FirstOrDefault();

            if (dto.AboutImage == null)
                throw new ArgumentNullException(nameof(dto.AboutImage), "About image file is required.");

            var aboutImagePath = await _fileStorageService.SaveOrReplaceAsync(dto.AboutImage, AboutFolder);

            About about;


            if (existingAbout != null)
            {
                _mapper.Map(dto, existingAbout);

                existingAbout.Update(
                    imagePath: aboutImagePath,
                    name: dto.Name,
                    profession: dto.Profession,
                    description: dto.Description,
                    birthday: dto.Birthday,
                    location: dto.Location,
                    phoneNumber: dto.PhoneNumber,
                    email: dto.Email,
                    languages: dto.Languages,
                    freelanceStatus: dto.FreelanceStatus
                    );
                about = existingAbout;
            }
            else
            {
                var mappedAbout = _mapper.Map<About>(dto);

                about = About.Create(
                   imagePath: aboutImagePath,
                   name: mappedAbout.Name,
                   profession: mappedAbout.Profession,
                   description: mappedAbout.Description,
                   birthday: mappedAbout.Birthday,
                   location: mappedAbout.Location,
                   phoneNumber: mappedAbout.PhoneNumber,
                   email: mappedAbout.Email,
                   languages: mappedAbout.Languages,
                   freelanceStatus: mappedAbout.FreelanceStatus
               );
                await _unitOfWork.AboutRepository.AddAsync(about);
            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AboutViewDto>(about);
        }
        #endregion

        #region get about
        public async Task<AboutViewDto?> GetAboutAsync()
        {
            var about = (await _unitOfWork.AboutRepository.GetAllAsync()).FirstOrDefault();

            if (about == null) return null;

            var aboutDto = _mapper.Map<AboutViewDto>(about);

            var aboutImageName = GetFileFromFolder(AboutFolder);
            aboutDto.AboutImagePath = aboutImageName != null ? $"{GetBaseUrl()}/{AboutFolder}/{aboutImageName}" : null;

            return aboutDto;
        }
        #endregion

        #region get files
        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }

        private string? GetFileFromFolder(string folderName)
        {
            var folderPath = Path.Combine(_env.WebRootPath, folderName);
            return Directory.Exists(folderPath)
                ? Directory.GetFiles(folderPath).Select(Path.GetFileName).FirstOrDefault()
                : null;
        }
        #endregion
    }
}
