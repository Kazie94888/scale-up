using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;

namespace ScaleUp.Core.Api.Webhooks.Haravan;

internal sealed class EndpointBuilder : IEndpointBuilder
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapWebHook<HaravanWebHook>("hooks/haravan");
    }
}
