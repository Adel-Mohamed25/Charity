using Charity.Contracts.Repositories;
using FluentValidation;

namespace Charity.Application.Features.V1.Role.Commands.CreateRole
{
    public class CreateRoleCommandValidation : AbstractValidator<CreateRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {
            RuleFor(cr => cr.RoleModel.Name)
                .NotNull().WithMessage("Name can not be null.")
                .NotEmpty().WithMessage("Name can not be empty.")
                .MaximumLength(50).WithMessage("Name can not exceed 50 characters.")
                .MustAsync(IsNameUnique).WithMessage("Name already exists.");


        }

        private async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.CharityRoles.IsExistAsync(u => u.Name!.Equals(name), cancellationToken);
        }
    }
}
