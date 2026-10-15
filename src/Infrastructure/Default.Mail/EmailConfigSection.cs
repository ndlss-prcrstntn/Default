using Default.Abstractions;

namespace Default.Mail;

public class EmailConfigSection : IConfigSection
{
    public static readonly string Name = "email";

    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 1025;

    public string SenderName { get; set; } = "Default";

    public string SenderEmail { get; set; } = "noreply@localhost";

    public string? ConnectionLogin { get; set; }

    public string? ConnectionPassword { get; set; }

    public bool UseAuthentication { get; set; }

    /// <summary>
    /// Implicit SSL (e.g. port 465). Mailpit local uses false.
    /// </summary>
    public bool EnableSsl { get; set; }

    public string Title { get; set; } = "Default";

    /// <summary>
    /// Public SPA origin used in confirmation links (e.g. https://example.com).
    /// </summary>
    public string PublicAppUrl { get; set; } = "http://localhost:4200";

    /// <summary>
    /// When true, register marks EmailConfirmed without waiting for the email link.
    /// Keep false in shared/staging/prod environments (including VPS Debug).
    /// </summary>
    public bool AutoConfirmOnRegister { get; set; }
}
