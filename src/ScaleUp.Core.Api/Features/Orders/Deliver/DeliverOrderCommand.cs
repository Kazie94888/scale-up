using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Deliver;

internal sealed class DeliverOrderCommand : ICommand<DeliverOrderResponse>
{
    [FromRoute(Name = "orderId")]
    public required Guid OrderId { get; init; }
    
    [FromBody]
    public required DeliverOrderRequest Request { get; init; } 
    public required UserInfoDto UserInfo { get; init; }
}

internal sealed class DeliverOrderRequest
{
    [JsonPropertyName("note")]
    public required string Note { get; init; }
}