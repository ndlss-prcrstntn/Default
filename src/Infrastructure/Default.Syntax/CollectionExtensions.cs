namespace Default.Syntax;

public static class CollectionExtensions
{
    public static void ForEach<T>(this IEnumerable<T>? source, Action<T> action)
    {
        if (source == null) return;

        foreach (var item in source) action(item);
    }

    public static T[]? AsArray<T>(this T source) => source == null ? null : [source];

    public static List<T>? AsList<T>(this T source) => source == null ? null : [source];

    public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
    {
        IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };

        return sequences.Aggregate(result,
            (current, sequence) => current.SelectMany(acc => sequence, (acc, item) => acc.Concat(new[] { item })));
    }
}