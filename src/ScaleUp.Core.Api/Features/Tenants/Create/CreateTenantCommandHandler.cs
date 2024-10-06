using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Tenants;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Tenants.Create;

public sealed class CreateTenantCommandHandler(MasterDataContext dataContext)
    : ICommandHandler<CreateTenantCommand, CreateTenantResponse>
{
    public async Task<Result<CreateTenantResponse>> Handle(CreateTenantCommand command,
        CancellationToken cancellationToken)
    {
        var tenantNameExisted =
            await dataContext.Tenants.AnyAsync(t => t.Name == command.Request.Name, cancellationToken);
        if (tenantNameExisted)
            return Result.Fail(string.Format(ErrorConstants.IsExisted, command.Request.Name));

        var tenantResult = Tenant.Create(command.Request.Name, command.Request.AdminEmail, command.Request.Phone,
            command.Request.Version, command.Request.ActivationState, command.Request.ActivationEndDate,
            command.UserInfo);

        if (tenantResult.IsFailed)
            return Result.Fail(tenantResult.Errors);
        
        dataContext.Tenants.Add(tenantResult.Value);
        
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateTenantResponse
        {
            TenantId = tenantResult.Value.Id
        });
    }
}