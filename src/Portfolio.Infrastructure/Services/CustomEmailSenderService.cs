using Microsoft.AspNetCore.Identity.UI.Services;

namespace Portfolio.Infrastructure.Services
{
    public class CustomEmailSenderService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Sending Email to {email} with subject {subject}");
            return Task.CompletedTask;
        }
    }
}
