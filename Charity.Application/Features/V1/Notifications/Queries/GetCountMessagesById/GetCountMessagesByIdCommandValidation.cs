using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Queries.GetCountMessagesById
{
    public class GetCountMessagesByIdCommandValidation : AbstractValidator<GetCountMessagesByIdCommand>
    {
        public GetCountMessagesByIdCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(gcm => gcm.ReceiveId)
                .NotNull().WithMessage(gcm => $"{nameof(gcm.ReceiveId)} can not be null.")
                .NotEmpty().WithMessage(gcm => $"{nameof(gcm.ReceiveId)} can not be empty.")
                .MaximumLength(36).WithMessage(gcm => $"{nameof(gcm.ReceiveId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(gcm => $"{nameof(gcm.ReceiveId)} can not less than 36 Characters.");

        }
    }
}
