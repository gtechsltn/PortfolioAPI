using FluentValidation;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class AboutDtoValidator : AbstractValidator<AboutCreateDto>
    {
        public AboutDtoValidator()
        {
            RuleFor(x => x.AboutImage)
                .NotNull().WithMessage("About image is required.")
                .Must(f => f.Length > 0).When(f => f != null).WithMessage("About image cannot be empty.")
                .Must(BeAValidImage).WithMessage("Only JPG, JPEG, or PNG images are allowed.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Profession)
                .NotEmpty().WithMessage("Profession is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Birthday)
                .NotEmpty().WithMessage("Birthday is required.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9\s\-]{7,15}$").WithMessage("Phone number is invalid.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.");

            RuleFor(x => x.Languages)
                .NotEmpty().WithMessage("Languages are required.");

            RuleFor(x => x.FreelanceStatus)
                .NotEmpty().WithMessage("Freelance status is required.");
        }

        private bool BeAValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }

        
    }
}
