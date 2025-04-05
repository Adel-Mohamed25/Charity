using Charity.Contracts.Repositories;
using FluentValidation;

namespace Charity.Application.Features.V1.Project.Commands.CreateProject
{
    public class CreateProjectCommandValidation : AbstractValidator<CreateProjectCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {
            RuleFor(cp => cp.ProjectModel.Name)
                .NotNull().WithMessage(cp => $"{nameof(cp.ProjectModel.Name)} can not be null.")
                .NotEmpty().WithMessage(cp => $"{nameof(cp.ProjectModel.Name)} can not be empty.")
                .MaximumLength(200).WithMessage(cp => $"{nameof(cp.ProjectModel.Name)} can not exceed 200 Characters.")
                .MinimumLength(3).WithMessage(cp => $"{nameof(cp.ProjectModel.Name)} can not less than 3 Characters.")
                .MustAsync(IsNameUnique).WithMessage(cp => $"{nameof(cp.ProjectModel.Name)} already exists.");


            RuleFor(cp => cp.ProjectModel.Description)
                .NotNull().WithMessage(cp => $"{nameof(cp.ProjectModel.Description)} can not be null.")
                .NotEmpty().WithMessage(cp => $"{nameof(cp.ProjectModel.Description)} can not be empty.")
                .MaximumLength(500).WithMessage(cp => $"{nameof(cp.ProjectModel.Description)} can not exceed 500 Characters.");


            RuleFor(cp => cp.ProjectModel.StartDate)
                .NotNull().WithMessage(cp => $"{nameof(cp.ProjectModel.StartDate)} can not be null.")
                .NotEmpty().WithMessage(cp => $"{nameof(cp.ProjectModel.StartDate)} can not be empty.");


            RuleFor(cp => cp.ProjectModel.ManagerId)
                .NotNull().WithMessage(cp => $"{nameof(cp.ProjectModel.ManagerId)} can not be null.")
                .NotEmpty().WithMessage(cp => $"{nameof(cp.ProjectModel.ManagerId)} can not be empty.")
                .MaximumLength(36).WithMessage(cp => $"{nameof(cp.ProjectModel.ManagerId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(cp => $"{nameof(cp.ProjectModel.ManagerId)} can not less than 36 Characters.");


            //RuleFor(cp => cp.ProjectModel.CreatedDate)
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage(cp => $"Invalid {nameof(cp.ProjectModel.CreatedDate)} cannot be in the future.");

        }

        private async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.Projects.IsExistAsync(p => p.Name.Equals(name), cancellationToken);
        }

    }
}
