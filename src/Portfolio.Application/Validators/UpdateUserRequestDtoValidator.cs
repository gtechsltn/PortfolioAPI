

using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
    {
        public UpdateUserRequestDtoValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.");
        }
    }
}
