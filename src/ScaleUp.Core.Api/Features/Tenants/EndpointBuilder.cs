using MediatR;
using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Api.Features.Tenants.Create;
using ScaleUp.Core.Api.Features.Tenants.Get;
using ScaleUp.Core.Api.Features.Tenants.GetList;
using ScaleUp.Core.Api.Features.Tenants.Update;

namespace ScaleUp.Core.Api.Features.Tenants;

public class EndpointBuilder : IEndpointBuilder
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/tenants", async (
            IMediator mediator,
            [AsParameters]
            GetTenantListQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapPost("/tenants", async (
            IMediator mediator,
            [AsParameters]
            CreateTenantCommand query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapGet("/tenants/{tenantId}", async (
            IMediator mediator,
            [AsParameters]
            GetTenantQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapPut("/tenants/{tenantId}", async (
            IMediator mediator,
            [AsParameters]
            UpdateTenantCommand query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
    }
}