using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.Get;

internal sealed class GetRoleQuery : IQuery<GetRoleQueryResponse>
{
    [FromRoute(Name = "roleId")]
    public Guid RoleId { get; set; }
}