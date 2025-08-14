
using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class SocialLinks : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string? LinkedIn { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }

        public static SocialLinks Create(
            string? linkedIn,
            string? twitter,
            string? instagram,
            string? facebook)
        {
            return new SocialLinks
            {
                LinkedIn = linkedIn,
                Twitter = twitter,
                Instagram = instagram,
                Facebook = facebook
            };
        }

        public void Update(
            string? linkedIn = null,
            string? twitter = null,
            string? instagram = null,
            string? facebook = null)
        {
            if (!string.IsNullOrWhiteSpace(linkedIn)) LinkedIn = linkedIn;
            if (!string.IsNullOrWhiteSpace(twitter)) Twitter = twitter;
            if (!string.IsNullOrWhiteSpace(instagram)) Instagram = instagram;
            if (!string.IsNullOrWhiteSpace(facebook)) Facebook = facebook;
        }
    }
}
