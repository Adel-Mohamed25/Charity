using FluentValidation;

namespace Charity.Application.Features.V1.MonetaryDonations.Queries.GetMonetaryDonationById
{
    public class GetMonetaryDonationByIdQueryValidation : AbstractValidator<GetMonetaryDonationByIdQuery>
    {
        public GetMonetaryDonationByIdQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(md => md.Id)
                .NotNull().WithMessage(md => $"{nameof(md.Id)} can not be null.")
                .NotEmpty().WithMessage(md => $"{nameof(md.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(md => $"{nameof(md.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(md => $"{nameof(md.Id)} can not less than 36 Characters.");
        }
    }
}
