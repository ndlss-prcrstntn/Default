using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Default.Cache.Abstractions;
using Default.DependencyInjection;
using StackExchange.Redis;

namespace Default.Cache;

public static class KeyDbServiceCollectionExtensions
{
    public static IServiceCollection AddCacheStorage(this IServiceCollection serviceCollection,
        IConfiguration configuration) =>
        serviceCollection
            .ConfigureSection<KeyDbConfigSection>(configuration)
            .AddSingleton<KeyDbConnectionFactory>()
            .AddSingleton<IConnectionMultiplexer>(sp =>
                sp.GetRequiredService<KeyDbConnectionFactory>().CreateMultiplexer())
            .AddTransient<ICacheStorage, CacheStorage>();
}