using FluentResults;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.SharedKernel.Caching;

public sealed class CacheService(IMemoryCache cache, ILogger<CacheService> logger) : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    public async Task<T?> Get<T>(string key, Func<Task<T>> loader, CancellationToken cancellationToken = default) where T : class
    {
        ArgumentNullException.ThrowIfNull(loader);

        if (cache.TryGetValue(key, out T? cachedValue))
        {
            return cachedValue;
        }

        var value = await loader();
        await Set(key, value, cancellationToken);

        return value;
    }

    public Task Set<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        cache.Set(key, value);
        return Task.CompletedTask;
    }

    public Task Remove(string key, CancellationToken cancellationToken = default)
    {
        cache.Remove(key);
        return Task.CompletedTask;
    }

    public async Task<Result<T>> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<Result<T>>> factory,
        TimeSpan? expiration = null,
        bool useSlidingExpiration = false,
        CancellationToken cancellationToken = default)
    {
        if (key.IsBlank())
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

        try
        {
            var value = await cache.GetOrCreateAsync(key, async entry =>
            {
                if (useSlidingExpiration)
                    entry.SetSlidingExpiration(expiration ?? DefaultExpiration);
                else
                    entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);

                var result = await factory(cancellationToken);

                // Avoid caching null or default values
                if (result.IsFailed)
                {
                    logger.LogWarning("Factory method returned a default value for key {Key}. Value not cached", key);
                    return default;
                }

                return result.Value;
            });

            if (EqualityComparer<T>.Default.Equals(value, default))
                return Result.Fail<T>("Value not found");
            return Result.Ok(value!);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching or creating cache for key {Key}", key);
            throw;
        }
    }
}