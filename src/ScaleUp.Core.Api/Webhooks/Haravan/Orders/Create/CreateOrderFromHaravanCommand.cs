using ScaleUp.Core.Application.Integrations.Haravan.Base;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Webhooks.Haravan.Orders.Create;

internal sealed class CreateOrderFromHaravanCommand : ICommand<Guid>
{
    public required HaravanWebHookInfoDto WebHookInfo { get; set; }

}