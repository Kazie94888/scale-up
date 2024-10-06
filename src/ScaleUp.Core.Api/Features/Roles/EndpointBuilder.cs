using MediatR;
using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Api.Features.Roles.Create;
using ScaleUp.Core.Api.Features.Roles.Get;
using ScaleUp.Core.Api.Features.Roles.GetFeaturePermissionList;
using ScaleUp.Core.Api.Features.Roles.GetList;
using ScaleUp.Core.Api.Features.Roles.Update;

namespace ScaleUp.Core.Api.Features.Roles;

internal sealed class EndpointBuilder : IEndpointBuilder
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/roles", async (
            IMediator mediator,
            [AsParameters] GetRoleListQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapPost("/roles", async (
            IMediator mediator,
            [AsParameters] CreateRoleCommand query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapGet("/roles/{roleId}", async (
            IMediator mediator,
            [AsParameters] GetRoleQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapPut("/roles/{roleId}", async (
            IMediator mediator,
            [AsParameters] UpdateRoleCommand query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapGet("/roles/feature-permissions", async (
            IMediator mediator,
            [AsParameters] GetFeaturePermissionListQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
    }
}