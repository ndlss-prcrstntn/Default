namespace Default.Mail;

/// <summary>Optional storefront branding for confirmation emails.</summary>
public sealed record ConfirmEmailBranding(
    string BrandName,
    string AccentColor,
    string FooterHost);

public static class EmailAppOrigin
{
    public const string DefaultPublicAppUrl = "http://localhost:4200";
    public const string DefaultBrandName = "Default";
    public const string DefaultAccentColor = "#c8f560";

    /// <summary>
    /// Apex <paramref name="publicAppUrl"/> for platform; <c>https://{slug}.{apexHost}</c>
    /// when storefront-scoped (skips loopback — local uses header scope on the same origin).
    /// </summary>
    public static string Resolve(
        string? publicAppUrl,
        bool storefrontScoped,
        string? storefrontSlug)
    {
        var apex = (string.IsNullOrWhiteSpace(publicAppUrl) ? DefaultPublicAppUrl : publicAppUrl)
            .Trim()
            .TrimEnd('/');

        if (!storefrontScoped || string.IsNullOrWhiteSpace(storefrontSlug))
            return apex;

        var slug = storefrontSlug.Trim().ToLowerInvariant();
        if (slug.Length is < 2 or > 63)
            return apex;

        if (!Uri.TryCreate(EnsureAbsolute(apex), UriKind.Absolute, out var uri))
            return apex;

        if (uri.IsLoopback
            || uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
            || uri.Host.EndsWith(".localhost", StringComparison.OrdinalIgnoreCase))
            return apex;

        var host = uri.Host;
        if (host.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            host = host[4..];

        var builder = new UriBuilder(uri.Scheme, $"{slug}.{host}") {
            Port = uri.IsDefaultPort ? -1 : uri.Port,
            Path = "",
            Query = "",
            Fragment = "",
        };
        return builder.Uri.GetLeftPart(UriPartial.Authority).TrimEnd('/');
    }

    public static ConfirmEmailBranding ResolveBranding(
        string? publicAppUrl,
        bool storefrontScoped,
        string? storefrontSlug,
        string? promoTeamName,
        string? accentColor)
    {
        var origin = Resolve(publicAppUrl, storefrontScoped, storefrontSlug);
        var host = Uri.TryCreate(EnsureAbsolute(origin), UriKind.Absolute, out var uri)
            ? uri.Host
            : "example.com";

        if (!storefrontScoped || string.IsNullOrWhiteSpace(storefrontSlug))
        {
            return new ConfirmEmailBranding(
                DefaultBrandName,
                DefaultAccentColor,
                host);
        }

        var brand = string.IsNullOrWhiteSpace(promoTeamName)
            ? storefrontSlug.Trim()
            : promoTeamName.Trim();
        var accent = NormalizeAccent(accentColor) ?? DefaultAccentColor;
        return new ConfirmEmailBranding(brand, accent, host);
    }

    private static string EnsureAbsolute(string url) =>
        url.Contains("://", StringComparison.Ordinal) ? url : $"https://{url}";

    private static string? NormalizeAccent(string? accent)
    {
        if (string.IsNullOrWhiteSpace(accent))
            return null;
        var t = accent.Trim();
        if (t.Length == 7 && t[0] == '#')
            return t.ToUpperInvariant();
        return null;
    }
}
