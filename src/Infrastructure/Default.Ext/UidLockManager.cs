using System.Collections.Concurrent;

namespace Default.Ext;

public class UidLockManager
{
    private static readonly ConcurrentDictionary<Guid, SemaphoreSlim> _locks = new();

    public async Task<IDisposable> LockAsync(Guid uid, CancellationToken ct = default)
    {
        var semaphore = _locks.GetOrAdd(uid, _ => new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync(ct);
        return new LockReleaser(uid);
    }

    private class LockReleaser(Guid uid) : IDisposable
    {
        public void Dispose()
        {
            if (!_locks.TryGetValue(uid, out var semaphore))
                return;

            semaphore.Release();

            if (semaphore.CurrentCount == 1)
                _locks.TryRemove(uid, out _);
        }
    }
}