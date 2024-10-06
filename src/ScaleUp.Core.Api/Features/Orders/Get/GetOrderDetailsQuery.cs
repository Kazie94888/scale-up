using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.Api.Features.Orders.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Get;

internal class GetOrderDetailsQuery : IQuery<OrderDetailsDto>
{
    [FromRoute(Name = "orderId")] public required Guid OrderId { get; init; }
}