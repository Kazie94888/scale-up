using MediatR;
using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Api.Features.Warehouses.GetList;

namespace ScaleUp.Core.Api.Features.Warehouses;

internal class EndpointBuilder : IEndpointBuilder
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/warehouses", async (
            IMediator mediator,
            [AsParameters] GetWarehouseListQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
    }
}