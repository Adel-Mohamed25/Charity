using FluentValidation;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Queries.GetAllVolunteersInActivity
{
    public class GetAllVolunteersInActivityQueryValidation : AbstractValidator<GetAllVolunteersInActivityQuery>
    {
        public GetAllVolunteersInActivityQueryValidation()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(gva => gva.VolunteerActivityId)
                .NotNull().WithMessage(gva => $"{nameof(gva.VolunteerActivityId)} can not be null.")
                .NotEmpty().WithMessage(gva => $"{nameof(gva.VolunteerActivityId)} can not be empty.")
                .MaximumLength(36).WithMessage(gva => $"{nameof(gva.VolunteerActivityId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(gva => $"{nameof(gva.VolunteerActivityId)} can not less than 36 Characters.");
        }
    }
}
