using FluentValidation;
using ScaleUp.Core.SharedKernel.Enums;

namespace ScaleUp.Core.Api.Features.Roles.Update;

internal sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Request.Name).NotEmpty();
        RuleFor(x => x.Request.Status).NotEmpty().Must(x => Enum.IsDefined(typeof(RoleStatus), x));
    }
}