using AutoMapper;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Service
{
    public class IntroService : IIntroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string IntroFolder = "IntroImages";
        private const string ResumeFolder = "ResumeFiles";

        public IntroService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileStorageService fileStorageService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region add or update intro
        public async Task<IntroViewDto> AddOrUpdateIntroAsync(IntroCreateDto dto)
        {
            var existingIntro = (await _unitOfWork.IntroRepository.GetAllAsync()).FirstOrDefault();
            if (dto.IntroImage == null) throw new ArgumentNullException(nameof(dto.IntroImage), "Intro image file is required.");
            var introImagePath = await _fileStorageService.SaveOrReplaceAsync(dto.IntroImage, IntroFolder);
            if (dto.UserResume == null) throw new ArgumentNullException(nameof(dto.UserResume), "Resume file is required.");
            var resumeFilePath = await _fileStorageService.SaveOrReplaceAsync(dto.UserResume, ResumeFolder);

            Intro intro;

            if (existingIntro != null)
            {
                _mapper.Map(dto, existingIntro);
                existingIntro.Update(
                    introName: dto.IntroName,
                    professionalTitle: dto.ProfessionalTitle,
                    introImagePath: introImagePath,
                    resumePath: resumeFilePath
                );
                intro = existingIntro;
            }
            else
            {
                var mappedIntro = _mapper.Map<Intro>(dto);

                intro = Intro.Create(
                    introName: mappedIntro.IntroName,
                    professionalTitle: mappedIntro.ProfessionalTitle,
                    introImagePath: introImagePath,
                    resumePath: resumeFilePath
                );
                await _unitOfWork.IntroRepository.AddAsync(intro);
            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<IntroViewDto>(intro);
        }
        #endregion

        #region get intro
        public async Task<IntroViewDto?> GetIntroAsync()
        {
            var intro = (await _unitOfWork.IntroRepository.GetAllAsync()).FirstOrDefault();
            if (intro == null) return null;

            var introDto = _mapper.Map<IntroViewDto>(intro);

            var introImageFullPath = BuildFileUrl(IntroFolder, intro.IntroImagePath);
            var resumeFullPath = BuildFileUrl(ResumeFolder, intro.ResumePath);

            introDto.IntroImagePath = introImageFullPath;
            introDto.ResumePath = resumeFullPath;

            return introDto;
        }
        #endregion

        #region get base url
        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }
        #endregion

        #region build file url
        private string? BuildFileUrl(string folder, string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return null;
            return $"{GetBaseUrl()}/{folder}/{fileName}";
        }
        #endregion

    }
}
