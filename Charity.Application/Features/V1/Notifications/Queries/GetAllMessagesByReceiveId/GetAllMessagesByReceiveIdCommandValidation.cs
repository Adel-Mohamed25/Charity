using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesByReceiveId
{
    public class GetAllMessagesByReceiveIdCommandValidation : AbstractValidator<GetAllMessagesByReceiveIdCommand>
    {
        public GetAllMessagesByReceiveIdCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(gam => gam.ReceiveId)
                .NotNull().WithMessage(gam => $"{nameof(gam.ReceiveId)} can not be null.")
                .NotEmpty().WithMessage(gam => $"{nameof(gam.ReceiveId)} can not be empty.")
                .MaximumLength(36).WithMessage(gam => $"{nameof(gam.ReceiveId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(gam => $"{nameof(gam.ReceiveId)} can not less than 36 Characters.");

        }
    }
}
