using MediatR;
using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Api.Features.Locations.GetList;

namespace ScaleUp.Core.Api.Features.Locations;

internal class EndpointBuilder : IEndpointBuilder
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {

        routeBuilder.MapGet("/locations", async (
            IMediator mediator,
            [AsParameters] GetLocationListQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
    }
}