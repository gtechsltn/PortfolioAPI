
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
namespace Portfolio.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditLogService _auditLogService;
        private const string ClientImageFolder = "ReviewedClientImage";

        public ReviewService(
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

        #region add review
        public async Task<ReviewViewDto> AddReviewAsync(ReviewCreateDto dto)
        {
            string? relativePath = null;
            string? fileNameOnly = null;

            if (dto.ClientImage != null)
            {
                relativePath = await _fileStorageService.GenerateFilePath(ClientImageFolder, dto.ClientImage.FileName);
                fileNameOnly = Path.GetFileName(relativePath);
            }

            _mapper.Map<Review>(dto);
            var review = Review.Create(
                clientReview: dto.ClientReview,
                clientImagePath: relativePath,
                clientName: dto.ClientName,
                clientProfession: dto.ClientProfession
            );
            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            if (dto.ClientImage != null && fileNameOnly != null)
            {
                await _fileStorageService.SaveFileAsync(dto.ClientImage, ClientImageFolder, fileNameOnly);
            }
            return _mapper.Map<ReviewViewDto>(review);
        }
        #endregion

        #region update reviewe
        public async Task<ReviewViewDto> UpdateReviewAsync(Guid id, ReviewCreateDto dto)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Review not found.");

            string updatedImagePath = review.ClientImagePath;

            if (dto.ClientImage != null)
            {
                if (!string.IsNullOrEmpty(review.ClientImagePath))
                {
                    var oldImageFileName = Path.GetFileName(review.ClientImagePath);
                    await _fileStorageService.DeleteFileAsync(oldImageFileName, ClientImageFolder);

                }

                updatedImagePath = await _fileStorageService.GenerateFilePath(ClientImageFolder, dto.ClientImage.FileName);
                var fileNameOnly = Path.GetFileName(updatedImagePath);
                await _fileStorageService.SaveFileAsync(dto.ClientImage, ClientImageFolder, fileNameOnly);
            }
            review.Update(
                clientReview: dto.ClientReview,
                clientImagePath: updatedImagePath,
                clientName: dto.ClientName,
                clientProfession: dto.ClientProfession
            );
            await _unitOfWork.ReviewRepository.UpdateAsync(review);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ReviewViewDto>(review);
        }
        #endregion

        #region get all review
        public async Task<IEnumerable<ReviewViewDto>> GetAllReviewsAsync()
        {
            var review = await _unitOfWork.ReviewRepository.GetAllAsync();

            var baseUrl = GetBaseUrl();

            return _mapper.Map<IEnumerable<ReviewViewDto>>(review)
            .Select(r => 
            {
                r.ClientImage = GenerateFullIconUrl(baseUrl, r.ClientImage);
                return r;
            })
            .ToList();
        }
        #endregion

        #region get review by id
        public async Task<ReviewViewDto> GetReviewByIdAsync(Guid id)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Review not found.");

            var reviewDto = _mapper.Map<ReviewViewDto>(review);

            if (!string.IsNullOrEmpty(reviewDto.ClientImage))
            {
                var baseUrl = GetBaseUrl();
                reviewDto.ClientImage = GenerateFullIconUrl(baseUrl, reviewDto.ClientImage);
            }
            return reviewDto;
        }
        #endregion

        #region delete review
        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Review not found.");
            if (!string.IsNullOrEmpty(review.ClientImagePath))
            {
                var filePath = Path.GetFileName(review.ClientImagePath);
                try
                {
                    await _fileStorageService.DeleteFileAsync(filePath, ClientImageFolder);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error deleting the review image file.", ex);
                }
            }

            var currentUserName = _currentUserService.GetCurrentUserName();
            var auditLog = AuditLogHelper.CreateDeleteLog(review, "Delete", "Review", currentUserName);
            await _auditLogService.AddAuditLogAsync(auditLog);

            await _unitOfWork.ReviewRepository.DeleteAsync(review);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
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
