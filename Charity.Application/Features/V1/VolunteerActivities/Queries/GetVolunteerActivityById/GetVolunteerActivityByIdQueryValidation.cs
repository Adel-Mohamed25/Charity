using FluentValidation;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetVolunteerActivityById
{
    public class GetVolunteerActivityByIdQueryValidation : AbstractValidator<GetVolunteerActivityByIdQuery>
    {
        public GetVolunteerActivityByIdQueryValidation()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(va => va.Id)
                .NotNull().WithMessage(va => $"{nameof(va.Id)} can not be null.")
                .NotEmpty().WithMessage(va => $"{nameof(va.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(va => $"{nameof(va.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(va => $"{nameof(va.Id)} can not less than 36 Characters.");

        }
    }
}
