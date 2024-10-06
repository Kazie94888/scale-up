using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.GetList;

internal sealed record GetUserListQuery : IQuery<GetUserListResponse>
{
    [FromQuery(Name = "page")]
    public int Page { get; set; }

    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; }
}