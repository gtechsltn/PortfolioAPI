using FluentValidation;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Portfolio.Application.Validators
{
    public class ServiceDtoValidator : AbstractValidator<ServicesCreateDto>
    {
        public ServiceDtoValidator() 
        {
            RuleFor(x => x.ServiceIcon)
               .NotNull().WithMessage("About image is required.")
               .Must(f => f.Length > 0).When(f => f != null).WithMessage("About image cannot be empty.")
               .Must(BeAValidIcon).WithMessage("Only JPG, JPEG, or PNG images are allowed.");

            RuleFor(x => x.ServiceName)
                .NotEmpty().WithMessage("Service name is required.")
                .MaximumLength(100).WithMessage("Service name cannot exceed 100 characters.");

            RuleFor(x => x.ServiceDestription)
                .NotEmpty().WithMessage("Service description is required.")
                .MaximumLength(500).WithMessage("Service description cannot exceed 500 characters.");

        }

        private bool BeAValidIcon(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }

    }
}
