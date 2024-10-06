using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Pack;

internal sealed class PackOrderCommand : ICommand<PackOrderResponse>
{
    [FromRoute(Name = "orderId")]
    public required Guid OrderId { get; init; }
    
    [FromBody]
    public required PackOrderRequest Request { get; init; }
    public required UserInfoDto UserInfo { get; init; }
}

internal sealed class PackOrderRequest
{
    [JsonPropertyName("note")]
    public required string Note { get; init; }
}