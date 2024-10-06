using System.Text.RegularExpressions;
using FluentValidation;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Extensions;

namespace ScaleUp.Core.Api.Features.Tenants.Update;

internal sealed class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantCommandValidator()
    {
        RuleFor(x => x.Request.Name).NotEmpty();
        RuleFor(x => x.Request.AdminEmail).EmailAddress();
        RuleFor(x => x.Request.Phone).Must(x => x is null || Regex.IsMatch(x, GlobalConstants.PhoneRegex));
        RuleFor(x => x.Request.Version).Must(x => Enum.IsDefined(typeof(TenantVersions), x));
        RuleFor(x => x.Request.ActivationState).Must(x => Enum.IsDefined(typeof(TenantActivationState), x));
        RuleFor(x => x.Request.ActivationEndDate).GreaterThan(DateTime.UtcNow);
        When(x => x.Request.ActivationState == TenantActivationState.ActiveWithLimitedTime.GetDescription(),
            () => { RuleFor(x => x.Request.ActivationEndDate).NotNull(); });
        When(x => x.Request.ActivationState == TenantActivationState.Active.GetDescription(),
            () => { RuleFor(x => x.Request.ActivationEndDate).Null(); });
    }
}