using FluentValidation;
using ScaleUp.Core.SharedKernel.Enums;

namespace ScaleUp.Core.Api.Features.Roles.Create;

internal sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Request.Name).NotEmpty();
        RuleFor(x => x.Request.Status).NotEmpty().Must(x => Enum.IsDefined(typeof(RoleStatus), x));
    }
}