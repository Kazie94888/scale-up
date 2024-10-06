using Microsoft.AspNetCore.Mvc;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Tenants.Get;

internal sealed record GetTenantQuery : IQuery<GetTenantResponse>
{
    [FromRoute(Name = "tenantId")]
    public Guid TenantId { get; set; }
}