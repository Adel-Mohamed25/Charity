using FluentValidation;

namespace Charity.Application.Features.V1.Notifications.Commands.SendToUser
{
    public class SendToUserCommandValidation : AbstractValidator<SendToUserCommand>
    {
        public SendToUserCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(sta => sta.NotificationModel.SenderId)
                .NotNull().WithMessage(sta => $"{nameof(sta.NotificationModel.SenderId)} can not be null.")
                .NotEmpty().WithMessage(sta => $"{nameof(sta.NotificationModel.SenderId)} can not be empty.")
                .MaximumLength(36).WithMessage(sta => $"{nameof(sta.NotificationModel.SenderId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(sta => $"{nameof(sta.NotificationModel.SenderId)} can not less than 36 Characters.");

            RuleFor(sta => sta.NotificationModel.ReceiverId)
                .NotNull().WithMessage(sta => $"{nameof(sta.NotificationModel.ReceiverId)} can not be null.")
                .NotEmpty().WithMessage(sta => $"{nameof(sta.NotificationModel.ReceiverId)} can not be empty.")
                .MaximumLength(36).WithMessage(sta => $"{nameof(sta.NotificationModel.ReceiverId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(sta => $"{nameof(sta.NotificationModel.ReceiverId)} can not less than 36 Characters.");

            RuleFor(sta => sta.NotificationModel.Message)
                .MaximumLength(500).WithMessage(sta => $"{nameof(sta.NotificationModel.Message)} can not exceed 500 characters.");
        }
    }
}
