using FluentValidation;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Commands.AddVolunteerToActivity
{
    public class AddVolunteerToActivityCommandValidation : AbstractValidator<AddVolunteerToActivityCommand>
    {
        public AddVolunteerToActivityCommandValidation()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(ava => ava.UserVolunteerActivity.VolunteerActivityId)
                .NotNull().WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.VolunteerActivityId)} can not be null.")
                .NotEmpty().WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.VolunteerActivityId)} can not be empty.")
                .MaximumLength(36).WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.VolunteerActivityId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.VolunteerActivityId)} can not less than 36 Characters.");

            RuleFor(ava => ava.UserVolunteerActivity.UserId)
                .NotNull().WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.UserId)} can not be null.")
                .NotEmpty().WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.UserId)} can not be empty.")
                .MaximumLength(36).WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.UserId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ava => $"{nameof(ava.UserVolunteerActivity.UserId)} can not less than 36 Characters.");

        }
    }
}
