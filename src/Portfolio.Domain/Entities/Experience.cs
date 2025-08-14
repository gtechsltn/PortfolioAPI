
using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Experience : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string Designation { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrentlyWorking { get; set; } = false;
        public string CompanyName { get; set; }
        public string WorkDetail { get; set; }

        public static Experience Create(
            string designation,
            DateOnly? startDate,
            DateOnly? endDate,
            bool isCurrentlyWorking,
            string companyName,
            string workDetail)
        {
            return new Experience
            {
                CompanyName = companyName,
                Designation = designation,
                StartDate = startDate,
                EndDate = endDate,
                IsCurrentlyWorking = isCurrentlyWorking,
                WorkDetail = workDetail
            };
        }

        public void Update(
            string designation = null,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            bool? isCurrentlyWorking = null,
            string companyName = null,
            string workDetail = null)
        {
            if (!string.IsNullOrWhiteSpace(designation)) Designation = designation;
            if (startDate.HasValue) StartDate = startDate.Value;
            EndDate = endDate;
            if (isCurrentlyWorking.HasValue) IsCurrentlyWorking = isCurrentlyWorking.Value;
            if (!string.IsNullOrWhiteSpace(companyName)) CompanyName = companyName;
            if (!string.IsNullOrWhiteSpace(workDetail)) WorkDetail = workDetail;
        }
    }
}
