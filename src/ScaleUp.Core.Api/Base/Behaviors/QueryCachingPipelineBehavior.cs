using FluentResults;
using MediatR;
using ScaleUp.Core.SharedKernel.Caching;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Base.Behaviors;

internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : ICachedQuery
{
    private readonly ICacheService _cacheService;

    public QueryCachingPipelineBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        return await _cacheService.GetOrCreateAsync(
            request.CacheKey,
            _ => next(),
            request.Expiration,
            useSlidingExpiration: true,
            cancellationToken);
    }
}