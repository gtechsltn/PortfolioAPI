

using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewViewDto> AddReviewAsync(ReviewCreateDto dto);
        Task<IEnumerable<ReviewViewDto>> GetAllReviewsAsync();
        Task<ReviewViewDto> UpdateReviewAsync(Guid id, ReviewCreateDto dto);
        Task DeleteReviewAsync(Guid id);
        Task<ReviewViewDto> GetReviewByIdAsync(Guid id);
    }
}
