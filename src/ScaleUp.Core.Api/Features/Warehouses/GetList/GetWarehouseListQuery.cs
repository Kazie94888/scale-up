using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Warehouses.GetList;

internal sealed class GetWarehouseListQuery : IQuery<GetWarehouseListResponse>
{
    [FromQuery(Name = "page")]
    public int Page { get; set; }

    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; }
}