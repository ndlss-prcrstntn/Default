namespace Default.Mail;

public interface IEmailService
{
    Task SendEmailAsync(
        string email,
        string title,
        string message,
        bool isHtml = false);

    Task SendConfirmEmailAsync(
        string email,
        string activationLink,
        string confirmationCode,
        ConfirmEmailBranding? branding = null);
}
