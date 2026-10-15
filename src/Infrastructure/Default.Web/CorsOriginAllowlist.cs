namespace Default.Web;

public static class CorsOriginAllowlist
{
    public const string PolicyName = "Frontend";

    public static string[] Normalize(IEnumerable<string> origins)
    {
        if (origins is null)
            return [];

        return origins
            .Where(static origin => !string.IsNullOrWhiteSpace(origin))
            .Select(static origin => origin.Trim().TrimEnd('/'))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    public static string[] Resolve(string scalarOrigins, IEnumerable<string> indexedOrigins)
    {
        if (!string.IsNullOrWhiteSpace(scalarOrigins) && !scalarOrigins.TrimStart().StartsWith('['))
        {
            var fromScalar = Normalize(scalarOrigins.Split(',',
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
            if (fromScalar.Length > 0)
                return fromScalar;
        }

        return Normalize(indexedOrigins);
    }

    public static bool IsAllowed(IEnumerable<string> allowedOrigins, string origin)
    {
        if (string.IsNullOrWhiteSpace(origin))
            return false;

        var allowed = Normalize(allowedOrigins);
        var candidate = origin.Trim().TrimEnd('/');

        foreach (var entry in allowed)
        {
            if (string.Equals(entry, candidate, StringComparison.OrdinalIgnoreCase))
                return true;

            if (MatchesWildcardOrigin(entry, candidate))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Supports patterns like <c>https://*.example.com</c> (single subdomain label only).
    /// Prod SPA+API are usually same-origin via Caddy on <c>{slug}.example.com</c>, so CORS
    /// may not be needed; this helps if <c>Cors__Origins</c> is ever expanded for cross-origin clients.
    /// </summary>
    internal static bool MatchesWildcardOrigin(string pattern, string origin)
    {
        const string marker = "://*.";
        var markerIndex = pattern.IndexOf(marker, StringComparison.Ordinal);
        if (markerIndex < 0)
            return false;

        var scheme = pattern[..markerIndex];
        var suffix = pattern[(markerIndex + marker.Length - 1)..]; // starts with '.'

        if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
            return false;

        if (!string.Equals(uri.Scheme, scheme, StringComparison.OrdinalIgnoreCase))
            return false;

        if (!uri.IsDefaultPort)
            return false;

        var host = uri.Host;
        if (!host.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            return false;

        var label = host[..^suffix.Length];
        return label.Length > 0 && !label.Contains('.');
    }
}
