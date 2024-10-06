using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Extensions;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Tenants.Update;

internal sealed class UpdateTenantCommandHandler(MasterDataContext dataContext)
    : ICommandHandler<UpdateTenantCommand, UpdateTenantResponse>
{
    public async Task<Result<UpdateTenantResponse>> Handle(UpdateTenantCommand command,
        CancellationToken cancellationToken)
    {
        var tenant = await dataContext.Tenants.FirstAsync(t => t.Id == command.TenantId, cancellationToken);

        if (!tenant.Name.EqualsTo(command.Request.Name))
        {
            var tenantNameExisted =
                await dataContext.Tenants.AnyAsync(t => t.Name == command.Request.Name, cancellationToken);
            if (tenantNameExisted)
                return Result.Fail(string.Format(ErrorConstants.IsExisted, command.Request.Name));
        }

        tenant.Update(command.Request.Name, command.Request.AdminEmail, command.Request.Phone, command.Request.Version,
            command.Request.ActivationState, command.Request.ActivationEndDate, command.UserInfo);

        dataContext.Tenants.Update(tenant);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new UpdateTenantResponse
        {
            TenantId = tenant.Id
        });
    }
}