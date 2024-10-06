using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.GetList;

internal sealed record GetOrderListQuery : IQuery<GetOrderListResponse>
{
    [FromQuery(Name = "page")]
    public required int Page { get; init; }

    [FromQuery(Name = "page_size")]
    public required int PageSize { get; init; }
}
