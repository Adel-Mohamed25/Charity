using FluentValidation;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetAllAssistanceRequestsById
{
    public class GetAllAssistanceRequestsByIdQueryValidation : AbstractValidator<GetAllAssistanceRequestsByIdQuery>
    {
        public GetAllAssistanceRequestsByIdQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ar => ar.BeneficiaryId)
                .NotNull().WithMessage(ar => $"{nameof(ar.BeneficiaryId)} can not be null.")
                .NotEmpty().WithMessage(ar => $"{nameof(ar.BeneficiaryId)} can not be empty.")
                .MaximumLength(36).WithMessage(ar => $"{nameof(ar.BeneficiaryId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ar => $"{nameof(ar.BeneficiaryId)} can not less than 36 Characters.");
        }
    }
}
