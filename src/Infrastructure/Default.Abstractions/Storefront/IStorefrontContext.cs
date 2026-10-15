namespace Default.Abstractions.Storefront;

/// <summary>Request-scoped promo storefront resolution (host / header / debug config).</summary>
public interface IStorefrontContext
{
    /// <summary>True when this request is bound to an enabled promo storefront.</summary>
    bool IsScoped { get; }

    /// <summary>
    /// True when a storefront slug was requested (host/header) but no enabled team matched.
    /// Must not fall through to the full catalog.
    /// </summary>
    bool IsUnavailable { get; }

    Guid? PromoTeamId { get; }

    string? Slug { get; }

    string? PromoTeamName { get; }

    Guid? MainPhotoId { get; }

    /// <summary>Optional storefront primary accent (#RRGGBB).</summary>
    string? AccentColor { get; }
}
