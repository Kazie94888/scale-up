using System.Text.RegularExpressions;
using FluentValidation;
using ScaleUp.Core.Api.Features.Users.Create;
using ScaleUp.Core.SharedKernel.Constants;
using ScaleUp.Core.SharedKernel.Enums;

namespace ScaleUp.Core.Api.Features.Users.Update;

internal sealed class UpdateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.User.FirstName).NotEmpty();
        RuleFor(x => x.User.LastName).NotEmpty();
        RuleFor(x => x.User.Phone).Must(x => x is null || Regex.IsMatch(x, GlobalConstants.PhoneRegex));
        RuleFor(x => x.User.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.User.Status).NotEmpty().Must(x => Enum.IsDefined(typeof(UserStatus), x));
        RuleFor(x => x.User.RoleIds).NotEmpty();
        RuleFor(x => x.User.WarehouseIds).NotEmpty();
    }
}