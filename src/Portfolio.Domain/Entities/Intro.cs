

using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Intro : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string? IntroName { get; set; }
        public string? ProfessionalTitle { get; set; }
        public string? IntroImagePath { get; set; }
        public string? ResumePath { get; set; }

        public static Intro Create(
            string? introName,
            string? professionalTitle,
            string? introImagePath,
            string? resumePath)
        {
            return new Intro
            {
                IntroName = introName,
                ProfessionalTitle = professionalTitle,
                IntroImagePath = introImagePath,
                ResumePath = resumePath
            };
        }
        public void Update(
            string? introName = null,
            string? professionalTitle = null,
            string? introImagePath = null,
            string? resumePath = null)
        {
            if (!string.IsNullOrWhiteSpace(introName)) IntroName = introName;
            if (!string.IsNullOrWhiteSpace(professionalTitle)) ProfessionalTitle = professionalTitle;
            if (!string.IsNullOrWhiteSpace(introImagePath)) IntroImagePath = introImagePath;
            if (!string.IsNullOrWhiteSpace(resumePath)) ResumePath = resumePath;
            
        }
    }
}
