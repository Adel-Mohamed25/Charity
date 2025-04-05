using FluentValidation;

namespace Charity.Application.Features.V1.VolunteerActivities.Commands.UpdateVolunteerActivity
{
    public class UpdateVolunteerActivityCommandValidation : AbstractValidator<UpdateVolunteerActivityCommand>
    {
        public UpdateVolunteerActivityCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(va => va.VolunteerActivityModel.OrganizerId)
                .NotNull().WithMessage(va => $"{nameof(va.VolunteerActivityModel.OrganizerId)} can not be null.")
                .NotEmpty().WithMessage(va => $"{nameof(va.VolunteerActivityModel.OrganizerId)} can not be empty.")
                .MaximumLength(36).WithMessage(va => $"{nameof(va.VolunteerActivityModel.OrganizerId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(va => $"{nameof(va.VolunteerActivityModel.OrganizerId)} can not less than 36 Characters.");

            RuleFor(va => va.VolunteerActivityModel.ActivityDescription)
                .NotNull().WithMessage(va => $"{nameof(va.VolunteerActivityModel.ActivityDescription)} can not be null.")
                .NotEmpty().WithMessage(va => $"{nameof(va.VolunteerActivityModel.ActivityDescription)} can not be empty.")
                .MaximumLength(500).WithMessage(va => $"{nameof(va.VolunteerActivityModel.ActivityDescription)} can not exceed 500 Characters.");

            //RuleFor(va => va.VolunteerActivityModel.CreatedDate)
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage(va => $"Invalid {nameof(va.VolunteerActivityModel.CreatedDate)} cannot be in the future.");

        }
    }
}
