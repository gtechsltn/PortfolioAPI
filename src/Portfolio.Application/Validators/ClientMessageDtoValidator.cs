using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class ClientMessageDtoValidator : AbstractValidator<ClientMessageCreateDto>
    {
        public ClientMessageDtoValidator() 
        {
            RuleFor(x => x.ClientName)
               .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.ClientEmail)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Email format is invalid.");

            RuleFor(x => x.ClientSubject)
               .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.ClientMessageContent)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(1200).WithMessage("Message must not exceed 1200 characters.");
        }
    }
}
