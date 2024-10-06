using MediatR;
using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Api.Features.Users.Create;
using ScaleUp.Core.Api.Features.Users.Get;
using ScaleUp.Core.Api.Features.Users.GetList;
using ScaleUp.Core.Api.Features.Users.GetLookup;
using ScaleUp.Core.Api.Features.Users.Update;

namespace ScaleUp.Core.Api.Features.Users;

internal class EndpointBuilder : IEndpointBuilder
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/users", async (
            IMediator mediator,
            [AsParameters] GetUserListQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapPost("/users", async (
            IMediator mediator,
            [AsParameters] CreateUserCommand command,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.ToHttpResult();
        });

        routeBuilder.MapGet("/users/{userId}", async (
            IMediator mediator,
            [AsParameters] GetUserQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
        
        routeBuilder.MapGet("/users/lookup", async (
            IMediator mediator,
            [AsParameters] GetUserLookupQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
        
        routeBuilder.MapPut("/users/{userId}", async (
            IMediator mediator,
            [AsParameters] UpdateUserCommand query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
    }
}