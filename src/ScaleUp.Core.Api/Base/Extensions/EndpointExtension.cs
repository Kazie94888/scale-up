using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace ScaleUp.Core.Api.Base.Extensions;

public static class EndpointExtension
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {

        var endpointServiceDescriptors = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false }
                && type.IsAssignableTo(typeof(IEndpointBuilder)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpointBuilder), type))
            .ToArray();

        services.TryAddEnumerable(endpointServiceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication application)
    {
        var endpoints = application.Services.GetRequiredService<IEnumerable<IEndpointBuilder>>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(application);
        }

        return application;
    }

    internal static void AddWebHook<TWebHook>(this IServiceCollection services)
        where TWebHook : class, IWebHook
    {
        services.TryAddTransient<TWebHook>();
    }

    internal static RouteHandlerBuilder MapWebHook<TWebHook>(this IEndpointRouteBuilder builder, string pattern)
        where TWebHook : class, IWebHook
    {
        return builder.MapPost(pattern, async (HttpContext context, TWebHook webHook) => await webHook.HandleAsync(context, context.RequestAborted));
    }

}