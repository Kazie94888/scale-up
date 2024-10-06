using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.GetList;

internal sealed class GetRoleListQuery : IQuery<GetRoleListResponse>
{
    [FromQuery(Name = "page")]
    public int Page { get; set; }

    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; }
}