using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Base.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;
using System.Text.Json.Serialization;

namespace ScaleUp.Core.Api.Features.Orders.Status.Update;
internal sealed class UpdateOrderStatusCommand : ICommand<UpdateOrderStatusResponse>
{
    [FromRoute(Name = "orderId")]
    public required Guid OrderId { get; init; }

    [FromBody]
    public required UpdateOrderStatusRequest Request { get; init; }

    public required UserInfoDto UserInfo { get; init; }
}

internal sealed class UpdateOrderStatusRequest
{
    [JsonPropertyName("note")]
    public required string Note { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }
}
