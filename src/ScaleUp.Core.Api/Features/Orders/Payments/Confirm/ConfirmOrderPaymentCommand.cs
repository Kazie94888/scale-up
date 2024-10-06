using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Payments.Confirm;

internal sealed class ConfirmOrderPaymentCommand : ICommand<ConfirmOrderPaymentResponse>
{
    [FromRoute(Name = "orderId")]
    public required Guid OrderId { get; init; }

    public required UserInfoDto UserInfo { get; init; }
}