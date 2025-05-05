using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesBySendId
{
    public class GetAllMessagesBySendIdCommandValidation : AbstractValidator<GetAllMessagesBySendIdCommand>
    {
        public GetAllMessagesBySendIdCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(gam => gam.SendId)
                .NotNull().WithMessage(gam => $"{nameof(gam.SendId)} can not be null.")
                .NotEmpty().WithMessage(gam => $"{nameof(gam.SendId)} can not be empty.")
                .MaximumLength(36).WithMessage(gam => $"{nameof(gam.SendId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(gam => $"{nameof(gam.SendId)} can not less than 36 Characters.");

        }
    }
}
