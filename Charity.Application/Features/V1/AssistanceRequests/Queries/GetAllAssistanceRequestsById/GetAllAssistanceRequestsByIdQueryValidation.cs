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
            RuleFor(ar => ar.Id)
                .NotNull().WithMessage(ar => $"{nameof(ar.Id)} can not be null.")
                .NotEmpty().WithMessage(ar => $"{nameof(ar.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(ar => $"{nameof(ar.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ar => $"{nameof(ar.Id)} can not less than 36 Characters.");
        }
    }
}
