using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Commands.UpdateMessage
{
    public class UpdateMessageCommandValidation : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(sta => sta.Notification.Id)
                .NotNull().WithMessage(sta => $"{nameof(sta.Notification.Id)} can not be null.")
                .NotEmpty().WithMessage(sta => $"{nameof(sta.Notification.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(sta => $"{nameof(sta.Notification.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(sta => $"{nameof(sta.Notification.Id)} can not less than 36 Characters.");

            RuleFor(sta => sta.Notification.SenderId)
                .NotNull().WithMessage(sta => $"{nameof(sta.Notification.SenderId)} can not be null.")
                .NotEmpty().WithMessage(sta => $"{nameof(sta.Notification.SenderId)} can not be empty.")
                .MaximumLength(36).WithMessage(sta => $"{nameof(sta.Notification.SenderId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(sta => $"{nameof(sta.Notification.SenderId)} can not less than 36 Characters.");

            RuleFor(sta => sta.Notification.ReceiverId)
                .MaximumLength(36).WithMessage(sta => $"{nameof(sta.Notification.ReceiverId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(sta => $"{nameof(sta.Notification.ReceiverId)} can not less than 36 Characters.");

            RuleFor(sta => sta.Notification.Message)
                .MaximumLength(500).WithMessage(sta => $"{nameof(sta.Notification.Message)} can not exceed 500 characters.");
        }
    }
}
