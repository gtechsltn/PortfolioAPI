

using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class ExperienceDtoValidator : AbstractValidator<ExperienceCreateDto>
    {
        public ExperienceDtoValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required.");

            RuleFor(x => x.Designation)
                .NotEmpty().WithMessage("Designation is required.");

            RuleFor(x => x.WorkDetail)
                .NotEmpty().WithMessage("Work detail is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            When(x => !x.IsCurrentlyWorking, () =>
            {
                RuleFor(x => x.EndDate)
                    .NotNull().WithMessage("End date is required when not currently working.")
                    .GreaterThan(x => x.StartDate).WithMessage("End date must be greater then start date.");
            });

            When(x => x.IsCurrentlyWorking, () =>
            {
                RuleFor(x => x.EndDate)
                    .Must(endDate => endDate == null)
                    .WithMessage("End date must be null when currently working.");
            });
        }
    }
}
