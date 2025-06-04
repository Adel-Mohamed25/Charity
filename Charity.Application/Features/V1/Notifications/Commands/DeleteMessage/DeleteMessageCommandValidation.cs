using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Commands.DeleteMessage
{
    public class DeleteMessageCommandValidation
        : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(cp => cp.messageId)
                .NotNull().WithMessage(cp => $"{nameof(cp.messageId)} can not be null.")
                .NotEmpty().WithMessage(cp => $"{nameof(cp.messageId)} can not be empty.")
                .MaximumLength(36).WithMessage(cp => $"{nameof(cp.messageId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(cp => $"{nameof(cp.messageId)} can not less than 36 Characters.");
        }
    }
}
