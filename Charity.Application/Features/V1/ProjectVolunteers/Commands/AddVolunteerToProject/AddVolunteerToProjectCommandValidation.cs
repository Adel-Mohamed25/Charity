using FluentValidation;

namespace Charity.Application.Features.V1.ProjectVolunteers.Commands.AddVolunteerToProject
{
    public class AddVolunteerToProjectCommandValidation : AbstractValidator<AddVolunteerToProjectCommand>
    {
        public AddVolunteerToProjectCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(avp => avp.ProjectVolunteer.ProjectId)
                .NotNull().WithMessage(avp => $"{nameof(avp.ProjectVolunteer.ProjectId)} can not be null.")
                .NotEmpty().WithMessage(avp => $"{nameof(avp.ProjectVolunteer.ProjectId)} can not be empty.")
                .MaximumLength(36).WithMessage(avp => $"{nameof(avp.ProjectVolunteer.ProjectId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(avp => $"{nameof(avp.ProjectVolunteer.ProjectId)} can not less than 36 Characters.");

            RuleFor(avp => avp.ProjectVolunteer.VolunteerId)
                .NotNull().WithMessage(avp => $"{nameof(avp.ProjectVolunteer.VolunteerId)} can not be null.")
                .NotEmpty().WithMessage(avp => $"{nameof(avp.ProjectVolunteer.VolunteerId)} can not be empty.")
                .MaximumLength(36).WithMessage(avp => $"{nameof(avp.ProjectVolunteer.VolunteerId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(avp => $"{nameof(avp.ProjectVolunteer.VolunteerId)} can not less than 36 Characters.");

        }
    }
}
