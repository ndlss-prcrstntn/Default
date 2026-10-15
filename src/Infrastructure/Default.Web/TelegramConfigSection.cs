using Default.Abstractions;

namespace Default.Web;

public class TelegramConfigSection : IConfigSection
{
    public static readonly string Name = "Telegram";

    /// <summary>Bot token from BotFather. Prefer env Telegram__BotToken / TELEGRAM_BOT_TOKEN.</summary>
    public string? BotToken { get; set; }

    /// <summary>Max age of initData auth_date (seconds). Default 24h.</summary>
    public int InitDataMaxAgeSeconds { get; set; } = 86_400;

    /// <summary>
    /// Public HTTPS base URL of this API (no trailing slash), used to register webhooks
    /// as {base}/telegram/webhook/{botUserId}. Empty = skip webhook registration.
    /// </summary>
    public string? WebhookPublicBaseUrl { get; set; }

    /// <summary>
    /// Optional secret_token for setWebhook; Telegram sends it as X-Telegram-Bot-Api-Secret-Token.
    /// Empty = webhook endpoint does not require the header.
    /// </summary>
    public string? WebhookSecret { get; set; }

    /// <summary>
    /// When true, long-poll getUpdates for all bots (local/dev). Skips webhook registration.
    /// </summary>
    public bool EnablePolling { get; set; }

    /// <summary>
    /// HTTPS origin for Mini App deep links (no trailing slash), e.g. https://example.com.
    /// Used for web_app buttons and Menu Button.
    /// </summary>
    public string? MiniAppPublicBaseUrl { get; set; } = "https://example.com";
}
