namespace Default.Cache.Abstractions;

public interface ICacheStorage
{
    string GetSearchPattern(string text);

    string GetKey(params object[] tokens);

    string GetKey<TService, TEntity>(params object[] tokens);

    Task<T?> GetAsync<T>(string key);

    T Get<T>(string key);

    Task<bool> AddAsync<T>(string key, T value, TimeSpan expireIn);

    bool Add<T>(string key, T value, TimeSpan expireIn);

    Task<bool> AddAsync<T>(string key, T value, DateTimeOffset expiresAt);

    Task<bool> RemoveAsync(string key);

    Task<bool> ExistsAsync(string key);
    bool Exists(string key);
    string[] SearchKeys(string pattern);
}