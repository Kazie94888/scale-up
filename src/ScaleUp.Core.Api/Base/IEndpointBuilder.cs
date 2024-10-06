namespace ScaleUp.Core.Api.Base;

internal interface IEndpointBuilder
{
    void MapEndpoint(IEndpointRouteBuilder routeBuilder);
}