using System.Globalization;
using System.Text.RegularExpressions;

namespace Default.DataAccess;

/// <summary>
/// Search key helper: lower-case, ё→е, collapsed whitespace.
/// Optional for entities with NameNormalized / NicknameNormalized columns.
/// </summary>
public static partial class SearchText
{
    public static string Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "";

        var lower = value.Trim().ToLower(CultureInfo.GetCultureInfo("ru-RU")).Replace('ё', 'е');
        return Whitespace().Replace(lower, " ");
    }

    [GeneratedRegex(@"\s+", RegexOptions.CultureInvariant)]
    private static partial Regex Whitespace();
}
