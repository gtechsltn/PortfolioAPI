
using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators
{
    public class SocialLinksDtoValidator : AbstractValidator<SocialLinksCreateDto>
    {
        public SocialLinksDtoValidator()
        {
            RuleFor(x => x.LinkedIn)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.LinkedIn))
                .WithMessage("LinkedIn URL must start with http:// or https://")
                .Matches(@"^https?://(www\.)?linkedin\.com/.*$")
                .When(x => !string.IsNullOrWhiteSpace(x.LinkedIn))
                .WithMessage("LinkedIn URL must be a valid linkedin.com link");

            RuleFor(x => x.Twitter)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.Twitter))
                .WithMessage("Twitter URL must start with http:// or https://")
                .Matches(@"^https?://(www\.)?twitter\.com/.*$")
                .When(x => !string.IsNullOrWhiteSpace(x.Twitter))
                .WithMessage("Twitter URL must be a valid twitter.com link");

            RuleFor(x => x.Instagram)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.Instagram))
                .WithMessage("Instagram URL must start with http:// or https://")
                .Matches(@"^https?://(www\.)?instagram\.com/.*$")
                .When(x => !string.IsNullOrWhiteSpace(x.Instagram))
                .WithMessage("Instagram URL must be a valid instagram.com link");

            RuleFor(x => x.Facebook)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.Facebook))
                .WithMessage("Facebook URL must start with http:// or https://")
                .Matches(@"^https?://(www\.)?facebook\.com/.*$")
                .When(x => !string.IsNullOrWhiteSpace(x.Facebook))
                .WithMessage("Facebook URL must be a valid facebook.com link");
        }

        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
    
}
