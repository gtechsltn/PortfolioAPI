
using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class SkillDetail : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string? SkillName { get; set; }
        public int Proficiency { get; set; }

        public static SkillDetail Create(
            string? skillName,
            int proficiency)
        {
            return new SkillDetail
            {
                SkillName = skillName,
                Proficiency = proficiency
            };
        }

        public void Update(string? skillName = null, int? proficiency = null)
        {
            if (!string.IsNullOrWhiteSpace(skillName)) SkillName = skillName;
            if (proficiency.HasValue) Proficiency = proficiency.Value;
        }
    }
}
