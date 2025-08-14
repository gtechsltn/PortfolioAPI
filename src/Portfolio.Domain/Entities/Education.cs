

using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Education : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string Qualification { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrentlyStudying { get; set; } = false;
        public string InstituteName { get; set; }
        public string EducationDetail { get; set; }
        public static Education Create(
            string qualification,
            DateOnly? startDate,
            DateOnly? endDate,
            bool isCurrentlyStudying,
            string instituteName,
            string educationDetail)
        {
            return new Education
            {
                Qualification = qualification,
                StartDate = startDate,
                EndDate = endDate,
                IsCurrentlyStudying = isCurrentlyStudying,
                InstituteName = instituteName,
                EducationDetail = educationDetail
            };
        }

        public void Update(
             string qualification = null,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            bool? isCurrentlyStudying = null,
            string instituteName = null,
            string educationDetail = null)
        {
            if (!string.IsNullOrWhiteSpace(qualification)) Qualification = qualification;
            if (startDate.HasValue) StartDate = startDate.Value;
            EndDate = endDate;
            if (isCurrentlyStudying.HasValue) IsCurrentlyStudying = isCurrentlyStudying.Value;
            if (!string.IsNullOrWhiteSpace(instituteName)) InstituteName = instituteName;
            if (!string.IsNullOrWhiteSpace(educationDetail)) EducationDetail = educationDetail;
        }
    }
    
}
