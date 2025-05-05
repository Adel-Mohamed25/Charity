using FluentValidation;

namespace Charity.Application.Features.V1.Authorization.Commands.AddUserToRole
{
    public class AddUserToRoleCommandValidation : AbstractValidator<AddUserToRoleCommand>
    {
        public AddUserToRoleCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(aur => aur.UserRoleModel.UserId)
                .NotNull().WithMessage(aur => $"{nameof(aur.UserRoleModel.UserId)} can not be null.")
                .NotEmpty().WithMessage(aur => $"{nameof(aur.UserRoleModel.UserId)} can not be empty.")
                .MaximumLength(36).WithMessage(aur => $"{nameof(aur.UserRoleModel.UserId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(aur => $"{nameof(aur.UserRoleModel.UserId)} can not less than 36 Characters.");

            RuleFor(aur => aur.UserRoleModel.RoleId)
                .NotNull().WithMessage(aur => $"{nameof(aur.UserRoleModel.RoleId)} can not be null.")
                .NotEmpty().WithMessage(aur => $"{nameof(aur.UserRoleModel.RoleId)} can not be empty.")
                .MaximumLength(36).WithMessage(aur => $"{nameof(aur.UserRoleModel.RoleId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(aur => $"{nameof(aur.UserRoleModel.RoleId)} can not less than 36 Characters.");

        }
    }
}
