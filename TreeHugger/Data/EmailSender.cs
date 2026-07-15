using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;

namespace TreeHugger.Data
{
    public class EmailSender(IFluentEmail FluentEmail) : IEmailSender<IdentityUser>
    {
        public async Task SendConfirmationLinkAsync(IdentityUser user, string toEmail, string callbackUrl)
        {
            var subject = "Confirm your email";
            var body = $"Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.";
            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendPasswordResetLinkAsync(IdentityUser user, string toEmail, string callbackUrl)
        {
            var subject = "Reset your password";
            var body = $"You can reset your password by clicking <a href='{callbackUrl}'>here</a>.";
            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendPasswordResetCodeAsync(IdentityUser user, string toEmail, string code)
        {
            var subject = "Reset your password";
            var body = $"Your password reset code is: {code}";
            await SendEmailAsync(toEmail, subject, body);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = await FluentEmail
                .To(toEmail)
                .Subject(subject)
                .Body(body, isHtml: true)
                .SendAsync();

            if (!email.Successful)
            {
                throw new InvalidOperationException($"Email sending failed: {string.Join(", ", email.ErrorMessages)}");
            }
        }
    }
}