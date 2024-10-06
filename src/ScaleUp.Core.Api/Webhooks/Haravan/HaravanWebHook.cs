using MediatR;
using ScaleUp.Core.Api.Base;
using ScaleUp.Core.Api.Base.Extensions;
using ScaleUp.Core.Application.Integrations.Haravan.Base;

namespace ScaleUp.Core.Api.Webhooks.Haravan;

internal sealed class HaravanWebHook(IMediator mediator) : IWebHook
{
    public async Task<IResult> HandleAsync(HttpContext context, CancellationToken cancellationToken)
    {
        var webHookInfo = await HaravanWebHookInfoDto.BindAsync(context);

        var command = HaravanCommandFactory.GetCommand(webHookInfo);

        var commandResult = await mediator.Send(command, cancellationToken);

        return commandResult.ToHttpResult();
    }
}
