using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Default.Cache.Abstractions;

namespace Default.Cache;

public class CacheStorageKey : ICacheStorageKey
{
    private static readonly ConcurrentDictionary<Type, string> TypeNames = new();

    public CacheStorageKey(params object[] segments)
    {
        Segments = segments.Select(Convert.ToString).ToArray();
    }

    public CacheStorageKey(Type serviceType, Type entityType, IEnumerable<object> segments)
    {
        var segmentList = new List<string> { FormatType(serviceType), FormatType(entityType) };

        segmentList.AddRange(segments.Select(Convert.ToString));

        Segments = [.. segmentList];
    }

    public string[] Segments { get; }

    public bool StartedWith(Type type) => Segments.First() == FormatType(type);

    public override string ToString() => Segments.Aggregate((f, s) => $"{f}:{s}");

    public static string FormatType(Type type)
    {
        if (TypeNames.TryGetValue(type, out var formattedKey)) return formattedKey;

        var keyName = type.Name;
        formattedKey = char.ToLower(keyName[0]) + Regex.Replace(
                keyName.Substring(1, keyName.Length - 1), "[A-Z]", "-$0", RegexOptions.None,
                TimeSpan.FromSeconds(10))
            .ToLowerInvariant();
        TypeNames.TryAdd(type, formattedKey);

        return formattedKey;
    }

    public static CacheStorageKey Parse(string key) => string.IsNullOrWhiteSpace(key)
        ? new CacheStorageKey()
        : new CacheStorageKey(key.Split(':'));
}