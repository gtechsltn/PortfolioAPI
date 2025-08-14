

using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class SkillDetailDtoValidator : AbstractValidator<SkillDetailCreateDto>
    {
        public SkillDetailDtoValidator()
        {
            RuleFor(x => x.SkillName)
            .NotEmpty().WithMessage("Skill name is required.")
            .MaximumLength(100).WithMessage("Skill name must not exceed 100 characters.");

            RuleFor(x => x.Proficiency)
                .InclusiveBetween(0, 100)
                .WithMessage("Proficiency must be between 0 and 100.");
        }
    }
}
