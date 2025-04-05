using FluentValidation;

namespace Charity.Application.Features.V1.ProjectVolunteers.Commands.RemoveVolunteerFromProject
{
    public class RemoveVolunteerFromProjectCommandValidation : AbstractValidator<RemoveVolunteerFromProjectCommand>
    {
        public RemoveVolunteerFromProjectCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(rvp => rvp.ProjectVolunteer.ProjectId)
                .NotNull().WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.ProjectId)} can not be null.")
                .NotEmpty().WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.ProjectId)} can not be empty.")
                .MaximumLength(36).WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.ProjectId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.ProjectId)} can not less than 36 Characters.");

            RuleFor(rvp => rvp.ProjectVolunteer.VolunteerId)
                .NotNull().WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.VolunteerId)} can not be null.")
                .NotEmpty().WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.VolunteerId)} can not be empty.")
                .MaximumLength(36).WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.VolunteerId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(rvp => $"{nameof(rvp.ProjectVolunteer.VolunteerId)} can not less than 36 Characters.");

        }
    }
}
