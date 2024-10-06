using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Confirm;

internal sealed class ConfirmOrderCommand : ICommand<ConfirmOrderResponse>
{
    [FromRoute(Name = "orderId")]
    public required Guid OrderId { get; init; }

    [FromBody] 
    public required ConfirmOrderRequest Request { get; init; }
    public required UserInfoDto UserInfo { get; init; }
}

internal sealed class ConfirmOrderRequest
{
    [JsonPropertyName("note")] 
    public required string Note { get; init; }
}