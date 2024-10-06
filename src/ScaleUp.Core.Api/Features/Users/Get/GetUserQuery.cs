using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.Get;

internal sealed record GetUserQuery : IQuery<GetUserResponse>
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
}