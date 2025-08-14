using AutoMapper;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;


namespace Portfolio.Infrastructure.Service
{
    public class ServicesService : IServicesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;
        private const string ServicesIconFolder = "ServicesIcon";

        public ServicesService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileStorageService fileStorageService,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserService currentUserService,
            IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
            _auditLogService = auditLogService;
        }
        #region add services
        public async Task<ServicesViewDto> AddServicesAsync(ServicesCreateDto dto)
        {
            var existingCount = (await _unitOfWork.ServiceRepository.GetAllAsync()).Count();

            Domain.Entities.Services services;
            if (existingCount >= 6)
            {
                throw new InvalidOperationException("You can only add up to 6 services.");
            }
            else
            {
                var relativePath = await _fileStorageService.GenerateFilePath(ServicesIconFolder, dto.ServiceIcon.FileName);
                var fileNameOnly = Path.GetFileName(relativePath);

                _mapper.Map<Domain.Entities.Services>(dto); //Add the domain because C# is mistakenly interpreting Services as a namespace not class
                var service = Domain.Entities.Services.Create(
                    serviceIconPath: relativePath,
                    serviceName: dto.ServiceName,
                    serviceDescription: dto.ServiceDestription
                );
                await _unitOfWork.ServiceRepository.AddAsync(service);
                await _unitOfWork.SaveChangesAsync();
                await _fileStorageService.SaveFileAsync(dto.ServiceIcon, ServicesIconFolder, fileNameOnly);
                return _mapper.Map<ServicesViewDto>(service);
            }
        }
        #endregion

        #region update services
        public async Task<ServicesViewDto> UpdateServiceAsync(Guid id, ServicesCreateDto dto)
        {
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("No services found.");

            string updatedFilePath = service.ServiceIconPath;

            if (dto.ServiceIcon != null)
            {
                if (!string.IsNullOrEmpty(service.ServiceIconPath))
                {
                    var oldFileName = Path.GetFileName(service.ServiceIconPath);
                    await _fileStorageService.DeleteFileAsync(ServicesIconFolder, oldFileName);
                }

                updatedFilePath = await _fileStorageService.GenerateFilePath(ServicesIconFolder, dto.ServiceIcon.FileName);
                var fileNameOnly = Path.GetFileName(updatedFilePath);
                await _fileStorageService.SaveFileAsync(dto.ServiceIcon, ServicesIconFolder, fileNameOnly);
            }

            service.Update(
                serviceName: dto.ServiceName,
                serviceDescription: dto.ServiceDestription,
                serviceIconPath: updatedFilePath
            );

            await _unitOfWork.ServiceRepository.UpdateAsync(service);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ServicesViewDto>(service);
        }
        #endregion

        #region get all services
        public async Task<IEnumerable<ServicesViewDto>> GetAllServicesAsync()
        {
            var services = await _unitOfWork.ServiceRepository.GetAllAsync();

            var baseUrl = GetBaseUrl();

            return _mapper.Map<IEnumerable<ServicesViewDto>>(services)
            .Select(s =>
            {
                s.ServiceIcon = GenerateFullIconUrl(baseUrl, s.ServiceIcon);
                return s;
            })
            .ToList();
        }

        #endregion

        #region delete services
        public async Task DeleteServiceAsync(Guid id)
        {
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Service not found.");

            if (!string.IsNullOrEmpty(service.ServiceIconPath))
            {
                var fileName = Path.GetFileName(service.ServiceIconPath);
                await _fileStorageService.DeleteFileAsync(ServicesIconFolder, fileName);
            }

            var currentUserName = _currentUserService.GetCurrentUserName();
            var auditLog = AuditLogHelper.CreateDeleteLog(service, "Delete", "Service", currentUserName);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.ServiceRepository.DeleteAsync(service);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                throw new InvalidOperationException("Failed to delete the review.");
            }
        }
        #endregion

        #region get base url
        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }
        #endregion

        #region generate full icon url
        private string GenerateFullIconUrl(string baseUrl, string? iconPath)
        {
            return !string.IsNullOrEmpty(iconPath)
                ? $"{baseUrl}/{iconPath}"
                : string.Empty;
        }
        #endregion
    }
}
