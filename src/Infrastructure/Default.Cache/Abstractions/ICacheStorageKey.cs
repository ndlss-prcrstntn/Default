namespace Default.Cache.Abstractions;

public interface ICacheStorageKey
{
    string[] Segments { get; }

    bool StartedWith(Type type);
}