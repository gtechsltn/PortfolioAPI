using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class HeaderDtoValidator : AbstractValidator<HeaderCreateDto>
    {
        public HeaderDtoValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9\s\-]{7,15}$").WithMessage("Phone number is invalid.");

            RuleFor(x => x.LogoImage)
                .NotNull().WithMessage("Logo image is required.");

            RuleFor(x => x.LogoImage)
                .Must(f => f.Length > 0)
                .When(f => f != null)
                .WithMessage("Logo image cannot be empty.");

        }
    }
}
