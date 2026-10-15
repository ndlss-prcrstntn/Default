namespace Default.Abstractions.Telegram;

/// <summary>
/// Registers / unregisters Telegram bots at runtime (commands, Menu Button, webhook).
/// Used after promo-team token save so bots work without API restart.
/// </summary>
public interface ITelegramBotRuntimeSync
{
    /// <summary>
    /// Apply token change for a promo bot. Pass plaintext BotFather tokens (never log them).
    /// <paramref name="previousPlaintextToken"/> is used to deleteWebhook when clearing or rotating.
    /// </summary>
    Task SyncPromoBotAsync(
        string? previousPlaintextToken,
        string? newPlaintextToken,
        Guid promoTeamId,
        string? slug,
        string? name,
        CancellationToken cancellationToken = default);
}

public sealed class NullTelegramBotRuntimeSync : ITelegramBotRuntimeSync
{
    public static NullTelegramBotRuntimeSync Instance { get; } = new();

    public Task SyncPromoBotAsync(
        string? previousPlaintextToken,
        string? newPlaintextToken,
        Guid promoTeamId,
        string? slug,
        string? name,
        CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
