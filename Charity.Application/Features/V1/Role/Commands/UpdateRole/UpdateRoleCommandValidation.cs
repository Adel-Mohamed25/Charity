﻿using Charity.Contracts.Repositories;
using FluentValidation;

namespace Charity.Application.Features.V1.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandValidation : AbstractValidator<UpdateRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {
            RuleFor(u => u.RoleModel.Id)
              .NotEmpty().WithMessage(u => $"{nameof(u.RoleModel.Id)} can not be not empty.")
              .NotNull().WithMessage(u => $"{nameof(u.RoleModel.Id)} can not be not null.")
              .MaximumLength(36).WithMessage(u => $"{nameof(u.RoleModel.Id)} can not be exceed 36 characters.")
              .MinimumLength(36).WithMessage(u => $"{nameof(u.RoleModel.Id)} can not be less than 36 characters.");

            RuleFor(u => u.RoleModel.Name)
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
