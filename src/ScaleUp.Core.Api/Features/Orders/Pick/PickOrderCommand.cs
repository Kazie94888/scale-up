using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Pick;

internal sealed class PickOrderCommand : ICommand<PickOrderResponse>
{
    [FromRoute(Name = "orderId")]
    public required Guid OrderId { get; init; }
    
    [FromBody]
    public required PickOrderRequest Request { get; init; }
    public required UserInfoDto UserInfo { get; init; }
}
internal sealed class PickOrderRequest
{
    [JsonPropertyName("note")]
    public required string Note { get; init; }
}