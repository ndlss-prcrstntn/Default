namespace Default.Abstractions.Storefront;

/// <summary>Encrypts per-promo Telegram bot tokens at rest. Never log plaintext.</summary>
public interface IPromoBotTokenProtector
{
    string Protect(string plaintext);

    string? TryUnprotect(string? cipher);
}
