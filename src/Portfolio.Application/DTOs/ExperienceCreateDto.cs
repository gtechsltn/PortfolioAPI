

namespace Portfolio.Application.DTOs
{
    public class ExperienceCreateDto
    {
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrentlyWorking { get; set; } = false;
        public string WorkDetail { get; set; }
    }
}
