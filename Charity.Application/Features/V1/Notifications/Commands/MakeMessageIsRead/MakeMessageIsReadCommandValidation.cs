using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Commands.MakeMessageIsRead
{
    public class MakeMessageIsReadCommandValidation : AbstractValidator<MakeMessageIsReadCommand>
    {
        public MakeMessageIsReadCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(m => m.MessageId)
                .NotNull().WithMessage(m => $"{nameof(m.MessageId)} can not be null.")
                .NotEmpty().WithMessage(m => $"{nameof(m.MessageId)} can not be empty.")
                .MaximumLength(36).WithMessage(m => $"{nameof(m.MessageId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(m => $"{nameof(m.MessageId)} can not less than 36 Characters.");

        }
    }
}
