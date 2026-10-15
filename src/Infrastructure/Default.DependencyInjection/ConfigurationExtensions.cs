using Microsoft.Extensions.Configuration;
using Default.Abstractions;
using Default.Syntax;

namespace Default.DependencyInjection;

public static class ConfigurationExtensions
{
    public static T GetConfigSection<T>(this IConfiguration configuration) where T : IConfigSection, new()
    {
        var key = typeof(T).GetStaticName();

        var section = configuration.GetSection(key).Get<T>();

        return section ?? new T();
    }
}