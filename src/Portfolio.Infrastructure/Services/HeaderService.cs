using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;



namespace Portfolio.Infrastructure.Services
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

        public async Task AddHeaderAsync(HeaderCreateDto dto)
        {
            var existingHeader = (await _unitOfWork.HeaderRepository.GetAllAsync()).FirstOrDefault();
            var dbPath = await _fileStorageService.SaveOrReplaceAsync(dto.LogoImage, HeaderFolder);

            if (existingHeader != null)
            {
                existingHeader.SetPhoneNumber(dto.PhoneNumber);
                existingHeader.SetLogoPath(dbPath);
                _unitOfWork.HeaderRepository.Update(existingHeader);
            }
            else
            {
                var mapp = _mapper.Map<Header>(dto);
                var header = Header.Create(mapp.PhoneNumber, dbPath);
                await _unitOfWork.HeaderRepository.AddAsync(header);
            }

            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<HeaderViewDto?> GetHeaderAsync()
        {
            var header = (await _unitOfWork.HeaderRepository.GetAllAsync()).FirstOrDefault();
            if (header == null) return null;

            var fileName = GetSingleImageFromFolder(HeaderFolder);
            var fullImageUrl = fileName != null ? $"{GetBaseUrl()}/{HeaderFolder}/{fileName}" : null;

            return new HeaderViewDto
            {
                PhoneNumber = header.PhoneNumber,
                LogoPath = fullImageUrl
            };
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }

        private string? GetSingleImageFromFolder(string folderName)
        {
            var folderPath = Path.Combine(_env.WebRootPath, folderName);
            return Directory.Exists(folderPath)
                ? Directory.GetFiles(folderPath).Select(Path.GetFileName).FirstOrDefault()
                : null;
        }
    }

}
