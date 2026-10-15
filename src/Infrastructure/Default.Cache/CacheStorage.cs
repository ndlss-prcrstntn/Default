using System.Text;
using Newtonsoft.Json;
using Default.Cache.Abstractions;
using StackExchange.Redis;

namespace Default.Cache;

public class CacheStorage(IConnectionMultiplexer connectionMultiplexer) : ICacheStorage
{
    public string GetSearchPattern(string text)
    {
        var sb = new StringBuilder();
        var chars = text.ToCharArray();

        sb.Append('*');

        foreach (var c in chars)
        {
            sb.Append('[');

            var isLowerCase = char.IsLower(c);

            sb.Append(isLowerCase ? c : char.ToLower(c));
            sb.Append(isLowerCase ? char.ToUpper(c) : c);

            sb.Append(']');
        }

        sb.Append('*');

        return sb.ToString();
    }

    public string GetKey(params object[] tokens) => new CacheStorageKey(tokens).ToString();

    public string GetKey<TService, TEntity>(params object[] tokens) =>
        new CacheStorageKey(typeof(TService), typeof(TEntity), tokens).ToString();

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await GetDatabase().StringGetAsync(key).ConfigureAwait(false);
        return !value.HasValue ? default : JsonConvert.DeserializeObject<T>(value);
    }

    public T Get<T>(string key)
    {
        var value = GetDatabase().StringGet(key);
        return !value.HasValue ? default : JsonConvert.DeserializeObject<T>(value);
    }

    public async Task<bool> AddAsync<T>(string key, T value, TimeSpan expireIn) => await GetDatabase()
        .StringSetAsync(key, JsonConvert.SerializeObject(value), expireIn).ConfigureAwait(false);

    public bool Add<T>(string key, T value, TimeSpan expireIn) => GetDatabase()
        .StringSet(key, JsonConvert.SerializeObject(value), expireIn);

    public async Task<bool> AddAsync<T>(string key, T value, DateTimeOffset expiresAt)
    {
        var expiration = expiresAt.UtcDateTime.Subtract(DateTime.UtcNow);

        if (expiration.TotalMicroseconds < 0)
            throw new ApplicationException($"Key expiration time {expiration} is negative");

        return await GetDatabase().StringSetAsync(key, JsonConvert.SerializeObject(value), expiration)
            .ConfigureAwait(false);
    }

    public async Task<bool> RemoveAsync(string key) => await GetDatabase().KeyDeleteAsync(key).ConfigureAwait(false);

    public Task<bool> ExistsAsync(string key) => GetDatabase().KeyExistsAsync(key);

    public bool Exists(string key) => GetDatabase().KeyExists(key);

    public string[] SearchKeys(string pattern)
    {
        var keys = GetServer().Keys(GetDatabase().Database, pattern, 1650);

        return keys.Select(key => key.ToString()).ToArray();
    }


    private IServer GetServer() => connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints().First());

    private IDatabase GetDatabase() => connectionMultiplexer.GetDatabase();
}