using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class ContactInfoDtoValidator : AbstractValidator<ContactInfoCreateDto>
    {
        public ContactInfoDtoValidator() 
        {
            RuleFor(x => x.ContactInfoDetail)
                .NotEmpty().WithMessage("Contact info is required.")
                .MaximumLength(500).WithMessage("Contact info must not exceed 500 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9\s\-]{7,15}$").WithMessage("Phone number is invalid.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
