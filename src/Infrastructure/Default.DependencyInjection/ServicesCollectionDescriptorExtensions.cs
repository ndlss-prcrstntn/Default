using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Default.Abstractions;
using Default.Syntax;

namespace Default.DependencyInjection;

public static class ServicesCollectionDescriptorExtensions
{
    public static IServiceCollection ConfigureSection<TConfigSection>(this IServiceCollection serviceCollection,
        IConfiguration configuration)
        where TConfigSection : class, IConfigSection, new() =>
        configuration == null
            ? serviceCollection
            : serviceCollection.Configure<TConfigSection>(
                configuration.GetSection(typeof(TConfigSection).GetStaticName()));
}