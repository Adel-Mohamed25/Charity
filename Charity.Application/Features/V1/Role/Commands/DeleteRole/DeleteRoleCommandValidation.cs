using FluentValidation;

namespace Charity.Application.Features.V1.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandValidation : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(d => d.Id)
               .NotEmpty().WithMessage(d => $"{nameof(d.Id)} can not be not empty.")
               .NotNull().WithMessage(d => $"{nameof(d.Id)} can not be not null.")
               .MaximumLength(36).WithMessage(d => $"{nameof(d.Id)} can not be exceed 36 characters.")
               .MinimumLength(36).WithMessage(d => $"{nameof(d.Id)} can not be less than 36 characters.");
        }
    }
}
