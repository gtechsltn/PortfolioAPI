

using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class ReviewDtoValidator : AbstractValidator<ReviewCreateDto>
    {
        public ReviewDtoValidator() 
        {
            RuleFor(x => x.ClientReview)
                .NotEmpty().WithMessage("Review is required.");

            RuleFor(x => x.ClientName)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.ClientProfession)
                .NotEmpty().WithMessage("Profession is required.");
        }
    }
}
