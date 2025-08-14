using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Portfolio.Infrastructure.Service
{
    public class HeaderService : IHeaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string HeaderFolder = "HeaderImages";

        public HeaderService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment env,
            IFileStorageService fileStorageService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _fileStorageService = fileStorageService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region add header
        public async Task<HeaderViewDto> AddOrUpdateHeaderAsync(HeaderCreateDto dto)
        {
            var existingHeader = (await _unitOfWork.HeaderRepository.GetAllAsync()).FirstOrDefault();
            var dbPath = await _fileStorageService.SaveOrReplaceAsync(dto.LogoImage, HeaderFolder);

            Header header;

            if (existingHeader != null)
            {
                existingHeader.Update(
                    phoneNumber: dto.PhoneNumber,
                    logoPath: dbPath);
                await _unitOfWork.HeaderRepository.UpdateAsync(existingHeader);
                header = existingHeader;
            }
            else
            {
                
                header = Header.Create(
                    phoneNumber: dto.PhoneNumber, 
                    logoPath: dbPath);

                await _unitOfWork.HeaderRepository.AddAsync(header);
            }

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<HeaderViewDto>(header);
        }
        #endregion

        #region get header
        public async Task<HeaderViewDto?> GetHeaderAsync()
        {
            var header = (await _unitOfWork.HeaderRepository.GetAllAsync()).FirstOrDefault();
            if (header == null) return null;

            var headerDto = _mapper.Map<HeaderViewDto>(header);

            var fileName = GetFileFromFolder(HeaderFolder);
            headerDto.LogoPath = fileName != null ? $"{GetBaseUrl()}/{HeaderFolder}/{fileName}" : null;

            return headerDto;
        }
        #endregion

        #region get base url
        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }
        #endregion

        #region get image from server side folder
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
