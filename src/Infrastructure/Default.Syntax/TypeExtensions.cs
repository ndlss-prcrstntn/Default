using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Default.Syntax;

public static class TypeExtensions
{
    public static string? TryGetStaticName(this Type type, string fieldName = "Name") =>
        type.GetField(fieldName, BindingFlags.Static | BindingFlags.Public)?.GetValue(null) as string;

    public static string GetStaticName(this Type type, string fieldName = "Name") =>
        type.TryGetStaticName(fieldName) ??
        throw new Exception($"Class doesn't have (public static string {fieldName}).");

    public static List<Type> GetDerivedClasses(this Type type)
    {
        var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a =>
            {
                try
                {
                    return a.GetTypes();
                }
                catch
                {
                    return [];
                }
            })
            .Where(t => type.IsAssignableFrom(t) && !t.IsAbstract &&
                        t.GetConstructor(Type.EmptyTypes) != null)
            .ToList();

        return derivedTypes;
    }

    public static List<Type> GetInstantiableDerivedTypes(this Type type)
    {
        var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a =>
            {
                try
                {
                    return a.GetTypes();
                }
                catch
                {
                    return [];
                }
            })
            .Where(t => type.IsAssignableFrom(t) && !t.IsAbstract)
            .ToList();

        return derivedTypes;
    }

    public static T DeepClone<T>(this T obj)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        var json = JsonSerializer.Serialize(obj, options);
        return JsonSerializer.Deserialize<T>(json, options)!;
    }
}