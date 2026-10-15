namespace Default.Abstractions.Storefront;

/// <summary>No-op storefront scope (platform / tests). Overridden by API middleware.</summary>
public sealed class NullStorefrontContext : IStorefrontContext
{
    public static NullStorefrontContext Instance { get; } = new();

    public bool IsScoped => false;
    public bool IsUnavailable => false;
    public Guid? PromoTeamId => null;
    public string? Slug => null;
    public string? PromoTeamName => null;
    public Guid? MainPhotoId => null;
    public string? AccentColor => null;
}
