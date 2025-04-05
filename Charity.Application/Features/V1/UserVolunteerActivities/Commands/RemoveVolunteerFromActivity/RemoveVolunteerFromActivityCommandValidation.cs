using FluentValidation;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Commands.RemoveVolunteerFromActivity
{
    public class RemoveVolunteerFromActivityCommandValidation : AbstractValidator<RemoveVolunteerFromActivityCommand>
    {
        public RemoveVolunteerFromActivityCommandValidation()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(rva => rva.UserVolunteerActivity.VolunteerActivityId)
                .NotNull().WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.VolunteerActivityId)} can not be null.")
                .NotEmpty().WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.VolunteerActivityId)} can not be empty.")
                .MaximumLength(36).WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.VolunteerActivityId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.VolunteerActivityId)} can not less than 36 Characters.");

            RuleFor(rva => rva.UserVolunteerActivity.UserId)
                .NotNull().WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.UserId)} can not be null.")
                .NotEmpty().WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.UserId)} can not be empty.")
                .MaximumLength(36).WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.UserId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(rva => $"{nameof(rva.UserVolunteerActivity.UserId)} can not less than 36 Characters.");

        }
    }
}
