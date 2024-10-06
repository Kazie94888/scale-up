using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Tenants.GetList;

internal sealed record GetTenantListQuery : IQuery<GetTenantListResponse>
{
    [FromQuery(Name = "page")]
    public int Page { get; set; }

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; }
}