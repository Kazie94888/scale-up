namespace ScaleUp.Core.Api.Base;

internal interface IWebHook
{
    Task<IResult> HandleAsync(HttpContext context, CancellationToken cancellationToken);
}
