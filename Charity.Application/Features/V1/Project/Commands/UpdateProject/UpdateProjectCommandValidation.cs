using FluentValidation;

namespace Charity.Application.Features.V1.Project.Commands.UpdateProject
{
    public class UpdateProjectCommandValidation : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(up => up.ProjectModel.Name)
                .NotNull().WithMessage(up => $"{nameof(up.ProjectModel.Name)} can not be null.")
                .NotEmpty().WithMessage(up => $"{nameof(up.ProjectModel.Name)} can not be empty.")
                .MaximumLength(200).WithMessage(up => $"{nameof(up.ProjectModel.Name)} can not exceed 200 Characters.")
                .MinimumLength(3).WithMessage(up => $"{nameof(up.ProjectModel.Name)} can not less than 3 Characters.");


            RuleFor(up => up.ProjectModel.Description)
                .NotNull().WithMessage(up => $"{nameof(up.ProjectModel.Description)} can not be null.")
                .NotEmpty().WithMessage(up => $"{nameof(up.ProjectModel.Description)} can not be empty.")
                .MaximumLength(500).WithMessage(up => $"{nameof(up.ProjectModel.Description)} can not exceed 500 Characters.");

            RuleFor(up => up.ProjectModel.ProjectStatus)
                .NotNull().WithMessage(up => $"{nameof(up.ProjectModel.ProjectStatus)} can not be null.")
                .NotEmpty().WithMessage(up => $"{nameof(up.ProjectModel.ProjectStatus)} can not be empty.")
                .IsInEnum().WithMessage(up => $"Invalid {nameof(up.ProjectModel.ProjectStatus)} value. Allowed values are Ongoing (1), Completed (2), Pending (3).");

            RuleFor(up => up.ProjectModel.StartDate)
                .NotNull().WithMessage(up => $"{nameof(up.ProjectModel.StartDate)} can not be null.")
                .NotEmpty().WithMessage(up => $"{nameof(up.ProjectModel.StartDate)} can not be empty.");


            RuleFor(up => up.ProjectModel.ManagerId)
                .NotNull().WithMessage(up => $"{nameof(up.ProjectModel.ManagerId)} can not be null.")
                .NotEmpty().WithMessage(up => $"{nameof(up.ProjectModel.ManagerId)} can not be empty.")
                .MaximumLength(36).WithMessage(up => $"{nameof(up.ProjectModel.ManagerId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(up => $"{nameof(up.ProjectModel.ManagerId)} can not less than 36 Characters.");



        }
    }
}
