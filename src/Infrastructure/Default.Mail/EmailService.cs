using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Default.Mail;

public class EmailService(
    ILogger<EmailService> logger,
    IOptions<EmailConfigSection> emailOptions) : IEmailService
{
    private readonly EmailConfigSection settings = emailOptions.Value;

    public async Task SendEmailAsync(
        string email,
        string title,
        string message,
        bool isHtml = false)
    {
        var mime = new MimeMessage();
        mime.From.Add(new MailboxAddress(settings.SenderName, settings.SenderEmail));
        mime.To.Add(MailboxAddress.Parse(email));
        mime.Subject = title;
        mime.Body = isHtml
            ? new TextPart("html") { Text = message }
            : new TextPart("plain") { Text = message };

        await SendAsync(mime);
    }

    public async Task SendConfirmEmailAsync(
        string email,
        string activationLink,
        string confirmationCode,
        ConfirmEmailBranding? branding = null)
    {
        var brand = branding ?? new ConfirmEmailBranding(
            EmailAppOrigin.DefaultBrandName,
            EmailAppOrigin.DefaultAccentColor,
            "example.com");

        var html = await LoadTemplateAsync("ConfirmEmailPage.html");
        html = html
            .Replace("{{activationLink}}", activationLink, StringComparison.Ordinal)
            .Replace("{{userId}}", confirmationCode, StringComparison.Ordinal)
            .Replace("{{confirmationCode}}", confirmationCode, StringComparison.Ordinal)
            .Replace("{{brandName}}", HtmlEncode(brand.BrandName), StringComparison.Ordinal)
            .Replace("{{accentColor}}", brand.AccentColor, StringComparison.Ordinal)
            .Replace("{{footerHost}}", HtmlEncode(brand.FooterHost), StringComparison.Ordinal);

        var plain = BuildConfirmEmailPlainText(activationLink, confirmationCode, brand);
        var title = $"{brand.BrandName}: подтвердите email";

        var mime = new MimeMessage();
        mime.From.Add(new MailboxAddress(settings.SenderName, settings.SenderEmail));
        mime.To.Add(MailboxAddress.Parse(email));
        mime.Subject = title;

        var builder = new BodyBuilder
        {
            HtmlBody = html,
            TextBody = plain,
        };
        mime.Body = builder.ToMessageBody();

        await SendAsync(mime);
    }

    private static string BuildConfirmEmailPlainText(
        string activationLink,
        string confirmationCode,
        ConfirmEmailBranding brand) =>
        string.Join(
            Environment.NewLine,
            $"{brand.BrandName} — подтверждение email",
            "",
            $"Остался один шаг: подтвердите адрес электронной почты для {brand.BrandName}.",
            "",
            $"Откройте ссылку: {activationLink}",
            "",
            $"Или введите код в приложении: {confirmationCode}",
            "",
            "Если вы не запрашивали это письмо, просто проигнорируйте его.",
            "",
            $"© {brand.BrandName} · {brand.FooterHost}");

    private static string HtmlEncode(string value) =>
        System.Net.WebUtility.HtmlEncode(value);

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        try
        {
            var secure = settings.EnableSsl
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.None;

            await client.ConnectAsync(settings.Host, settings.Port, secure);

            if (settings.UseAuthentication)
            {
                var user = settings.ConnectionLogin ?? settings.SenderEmail;
                await client.AuthenticateAsync(user, settings.ConnectionPassword ?? "");
            }

            await client.SendAsync(message);
            logger.LogInformation("Sent email to {Recipient} via {Host}:{Port}", message.To, settings.Host, settings.Port);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {Recipient} via {Host}:{Port}", message.To, settings.Host, settings.Port);
            throw;
        }
        finally
        {
            if (client.IsConnected)
                await client.DisconnectAsync(true);
        }
    }

    private static async Task<string> LoadTemplateAsync(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Pages", fileName);
        if (!File.Exists(path))
        {
            // Fallback when running from project output without copied Pages.
            var alt = Path.Combine(AppContext.BaseDirectory, "Mail", "Pages", fileName);
            if (File.Exists(alt))
                path = alt;
            else
                throw new FileNotFoundException($"Email template not found: {fileName}", path);
        }

        return await File.ReadAllTextAsync(path);
    }
}
