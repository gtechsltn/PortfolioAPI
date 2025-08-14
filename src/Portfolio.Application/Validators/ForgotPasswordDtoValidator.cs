

using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordRequestDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email format is invalid.");
        }
    }
}
