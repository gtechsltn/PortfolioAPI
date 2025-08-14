

using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class EducationDtoValidator : AbstractValidator<EducationCreateDto>
    {
        public EducationDtoValidator() 
        {
            RuleFor(x => x.InstituteName)
               .NotEmpty().WithMessage("Institute name is required.");

            RuleFor(x => x.Qualification)
                .NotEmpty().WithMessage("Qualification is required.");

            RuleFor(x => x.EducationDetail)
                .NotEmpty().WithMessage("Education detail is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            When(x => !x.IsCurrentlyStudying, () =>
            {
                RuleFor(x => x.EndDate)
                    .NotNull().WithMessage("End date is required when not currently studying.")
                    .GreaterThan(x => x.StartDate).WithMessage("End date must be greater then start date.");
            });

            When(x => x.IsCurrentlyStudying, () =>
            {
                RuleFor(x => x.EndDate)
                    .Must(endDate => endDate == null)
                    .WithMessage("End date must be null when currently studying.");
            });
        }
    }
}
