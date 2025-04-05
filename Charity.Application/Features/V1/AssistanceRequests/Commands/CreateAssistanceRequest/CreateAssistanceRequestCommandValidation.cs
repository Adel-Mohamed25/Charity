using FluentValidation;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.CreateAssistanceRequest
{
    public class CreateAssistanceRequestCommandValidation : AbstractValidator<CreateAssistanceRequestCommand>
    {
        public CreateAssistanceRequestCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {

            RuleFor(ca => ca.CreateAssistance.BeneficiaryId)
                .NotNull().WithMessage(ca => $"{nameof(ca.CreateAssistance.BeneficiaryId)} can not be null.")
                .NotEmpty().WithMessage(ca => $"{nameof(ca.CreateAssistance.BeneficiaryId)} can not be empty.")
                .MaximumLength(36).WithMessage(ca => $"{nameof(ca.CreateAssistance.BeneficiaryId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ca => $"{nameof(ca.CreateAssistance.BeneficiaryId)} can not less than 36 Characters.");

            RuleFor(ca => ca.CreateAssistance.RequestDetails)
                .MaximumLength(500).WithMessage(ca => $"{nameof(ca.CreateAssistance.RequestDetails)} can not exceed 500 Characters.");

            RuleFor(ca => ca.CreateAssistance.InKindDonationId)
                .MaximumLength(36).WithMessage(ca => $"{nameof(ca.CreateAssistance.InKindDonationId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ca => $"{nameof(ca.CreateAssistance.InKindDonationId)} can not less than 36 Characters.");
        }
    }
}
