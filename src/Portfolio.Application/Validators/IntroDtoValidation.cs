

using FluentValidation;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class IntroDtoValidation : AbstractValidator<IntroCreateDto>
    {
        public IntroDtoValidation()
        {
            RuleFor(x => x.IntroName)
            .NotEmpty().WithMessage("Intro name is required.")
            .MaximumLength(100).WithMessage("Intro name must not exceed 100 characters.");

            RuleFor(x => x.ProfessionalTitle)
                .NotEmpty().WithMessage("Professional title is required.")
                .MaximumLength(100).WithMessage("Professional title must not exceed 100 characters.");

            RuleFor(x => x.IntroImage)
                .NotNull().WithMessage("Intro image is required.")
                .Must(BeAValidImage).WithMessage("Only image files (jpg, jpeg, png) are allowed.")
                .Must(f => f.Length <= 2 * 1024 * 1024)
                .WithMessage("Image size must be less than or equal to 2MB.");

            RuleFor(x => x.UserResume)
                .NotNull().WithMessage("Resume file is required.")
                .Must(f => f.Length > 0).When(f => f != null).WithMessage("Resume file cannot be empty.")
                .Must(BePdf).WithMessage("Only PDF files are allowed for the resume.");

        }
        private bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }
        private bool BePdf(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return extension == ".pdf";
        }
    }
}
