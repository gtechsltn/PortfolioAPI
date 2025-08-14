

using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class SkillSectionDtoValidator : AbstractValidator<SkillSectionCreateDto>
    {
        public SkillSectionDtoValidator()
        {
            RuleFor(x => x.SkillSectionTitle)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

            RuleFor(x => x.SkillSectionDescription)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
