using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Commands.SoftDeleteMessage
{
    public class SoftDeleteMessageCommandValidation : AbstractValidator<SoftDeleteMessageCommand>
    {
        public SoftDeleteMessageCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(n => n.MessageId)
                .NotNull().WithMessage(n => $"{nameof(n.MessageId)} can not be null.")
                .NotEmpty().WithMessage(n => $"{nameof(n.MessageId)} can not be empty.")
                .MaximumLength(36).WithMessage(n => $"{nameof(n.MessageId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(n => $"{nameof(n.MessageId)} can not less than 36 Characters.");
        }
    }
}
