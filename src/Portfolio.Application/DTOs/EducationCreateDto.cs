

namespace Portfolio.Application.DTOs
{
    public class EducationCreateDto
    {
        public string Qualification { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrentlyStudying { get; set; } = false;
        public string InstituteName { get; set; }
        public string EducationDetail { get; set; }
    }
}
