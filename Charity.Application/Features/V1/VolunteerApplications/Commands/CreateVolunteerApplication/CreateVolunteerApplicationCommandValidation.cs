using Charity.Application.Features.V1.VolunteerApplications.Commands.CreateVolunteerApplication;
using Charity.Contracts.Repositories;
using FluentValidation;

namespace Charity.Applivation.Features.V1.VolunteerApplivations.Commands.CreateVolunteerApplivation
{
    public class CreateVolunteerApplivationCommandValidation : AbstractValidator<CreateVolunteerApplicationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateVolunteerApplivationCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {
            RuleFor(va => va.VolunteerApplicationModel.VolunteerId)
                .NotNull().WithMessage(va => $"{nameof(va.VolunteerApplicationModel.VolunteerId)} van not be null.")
                .NotEmpty().WithMessage(va => $"{nameof(va.VolunteerApplicationModel.VolunteerId)} van not be empty.")
                .MaximumLength(36).WithMessage(va => $"{nameof(va.VolunteerApplicationModel.VolunteerId)} van not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(va => $"{nameof(va.VolunteerApplicationModel.VolunteerId)} van not less than 36 Characters.");

            RuleFor(va => va.VolunteerApplicationModel.RequestDetails)
                .MaximumLength(500).WithMessage(va => $"{nameof(va.VolunteerApplicationModel.RequestDetails)} van not exceed 500 Characters.");

            RuleFor(va => va.VolunteerApplicationModel.VolunteerActivityId)
                .MaximumLength(36).WithMessage(va => $"{nameof(va.VolunteerApplicationModel.VolunteerActivityId)} van not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(va => $"{nameof(va.VolunteerApplicationModel.VolunteerActivityId)} van not less than 36 Characters.");

            RuleFor(va => va.VolunteerApplicationModel.ProjectId)
                .MaximumLength(36).WithMessage(va => $"{nameof(va.VolunteerApplicationModel.ProjectId)} van not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(va => $"{nameof(va.VolunteerApplicationModel.ProjectId)} van not less than 36 Characters.");

            RuleFor(v => v.VolunteerApplicationModel)
                .MustAsync((application, cancellation) =>
                    IsApplicationUnique(
                        application.VolunteerId,
                        application.VolunteerActivityId,
                        application.ProjectId,
                        cancellation))
                .WithMessage("This volunteer has already applied for this activity or project.");

        }

        private async Task<bool> IsApplicationUnique(string volunteerId,
            string? volunteerActivityId,
            string? projectId,
            CancellationToken cancellationToken)
        {
            return !await _unitOfWork.VolunteerApplications.IsExistAsync(v =>
                v.VolunteerId == volunteerId &&
                (
                    (volunteerActivityId != null && v.VolunteerActivityId == volunteerActivityId) ||
                    (projectId != null && v.ProjectId == projectId)
                ),
                cancellationToken
            );
        }
    }
}
