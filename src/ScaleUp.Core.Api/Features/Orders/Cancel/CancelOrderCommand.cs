using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;
using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Cancel;

internal sealed class CancelOrderCommand : ICommand<CancelOrderResponse>
{
    [FromRoute(Name = "orderId")]
    public required Guid OrderId { get; init; }

    [FromBody]
    public required CancelOrderRequest Request { get; init; }

    public required UserInfoDto UserInfo { get; init; }
}

internal sealed record CancelOrderRequest
{
    [JsonPropertyName("cancel_reason_code")]
    public required string CancelReasonCode { get; init; }

    [JsonPropertyName("note")] 
    public required string Note { get; init; }
}