using ScaleUp.Core.Api.Webhooks.Haravan.Orders.Create;
using ScaleUp.Core.Application.Integrations.Haravan.Base;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Webhooks.Haravan
{
    internal class HaravanCommandFactory
    {
        internal static ICommand<Guid> GetCommand(HaravanWebHookInfoDto webHookInfo)
        {
            switch (webHookInfo.Type)
            {
                case var type when type == HaravanWebHookType.OrderCreated:
                    {

                        return new CreateOrderFromHaravanCommand
                        {
                            WebHookInfo = webHookInfo
                        };
                    }
                default: throw new NotImplementedException();
            }
        }
    }
}
