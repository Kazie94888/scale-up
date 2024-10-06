using FluentResults;

namespace ScaleUp.Core.SharedKernel.Caching;

public interface ICacheService
{
    Task<T?> Get<T>(string key, Func<Task<T>> loader, CancellationToken cancellationToken = default) where T : class;
    Task Set<T>(string key, T value, CancellationToken cancellationToken = default) where T : class;

    Task Remove(string key, CancellationToken cancellationToken = default);

    Task<Result<T>> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<Result<T>>> factory,
        TimeSpan? expiration = null,
        bool useSlidingExpiration = false,
        CancellationToken cancellationToken = default);
}