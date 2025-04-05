using FluentValidation;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.UpdateAssistanceRequest
{
    public class UpdateAssistanceRequestCommandValidation : AbstractValidator<UpdateAssistanceRequestCommand>
    {
        public UpdateAssistanceRequestCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ca => ca.AssistanceRequest.Id)
                .NotNull().WithMessage(ca => $"{nameof(ca.AssistanceRequest.Id)} can not be null.")
                .NotEmpty().WithMessage(ca => $"{nameof(ca.AssistanceRequest.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(ca => $"{nameof(ca.AssistanceRequest.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ca => $"{nameof(ca.AssistanceRequest.Id)} can not less than 36 Characters.");


            RuleFor(ca => ca.AssistanceRequest.BeneficiaryId)
                .NotNull().WithMessage(ca => $"{nameof(ca.AssistanceRequest.BeneficiaryId)} can not be null.")
                .NotEmpty().WithMessage(ca => $"{nameof(ca.AssistanceRequest.BeneficiaryId)} can not be empty.")
                .MaximumLength(36).WithMessage(ca => $"{nameof(ca.AssistanceRequest.BeneficiaryId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ca => $"{nameof(ca.AssistanceRequest.BeneficiaryId)} can not less than 36 Characters.");

            RuleFor(ca => ca.AssistanceRequest.RequestDetails)
                .MaximumLength(500).WithMessage(ca => $"{nameof(ca.AssistanceRequest.RequestDetails)} can not exceed 500 Characters.");

            RuleFor(ca => ca.AssistanceRequest.InKindDonationId)
                .MaximumLength(36).WithMessage(ca => $"{nameof(ca.AssistanceRequest.InKindDonationId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ca => $"{nameof(ca.AssistanceRequest.InKindDonationId)} can not less than 36 Characters.");

            RuleFor(ca => ca.AssistanceRequest.RequestStatus)
                .NotNull().WithMessage(ca => $"{nameof(ca.AssistanceRequest.RequestStatus)} can not be null.")
                .NotEmpty().WithMessage(ca => $"{nameof(ca.AssistanceRequest.RequestStatus)} can not be empty.")
                .IsInEnum().WithMessage(ca => $"{nameof(ca.AssistanceRequest.RequestStatus)} value. Allowed values are Approved (1), Rejected (2), Pending(3).");

            RuleFor(ca => ca.AssistanceRequest.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(ca => $"Invalid {nameof(ca.AssistanceRequest.CreatedDate)} cannot be in the future.");

            RuleFor(ca => ca.AssistanceRequest.ModifiedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(ca => $"Invalid {nameof(ca.AssistanceRequest.ModifiedDate)} cannot be in the future.");

        }
    }
}
