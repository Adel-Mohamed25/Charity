using Charity.Application.Features.V1.VolunteerApplications.Commands.UpdateVolunteerApplication;
using FluentValidation;

namespace Charity.Applivation.Features.V1.VolunteerApplivations.Commands.UpdateVolunteerApplivation
{
    public class UpdateVolunteerApplivationCommandValidation : AbstractValidator<UpdateVolunteerApplicationCommand>
    {
        public UpdateVolunteerApplivationCommandValidation()
        {
            ApplyValidationRules();
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

            RuleFor(ca => ca.VolunteerApplicationModel.RequestStatus)
                .NotNull().WithMessage(ca => $"{nameof(ca.VolunteerApplicationModel.RequestStatus)} can not be null.")
                .NotEmpty().WithMessage(ca => $"{nameof(ca.VolunteerApplicationModel.RequestStatus)} can not be empty.")
                .IsInEnum().WithMessage(ca => $"{nameof(ca.VolunteerApplicationModel.RequestStatus)} value. Allowed values are Approved (1), Rejected (2), Pending(3).");


            RuleFor(va => va.VolunteerApplicationModel.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(va => $"Invalid {nameof(va.VolunteerApplicationModel.CreatedDate)} vannot be in the future.");

        }
    }
}
