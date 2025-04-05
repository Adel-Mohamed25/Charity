using FluentValidation;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetinKindDonationById
{
    public class GetinKindDonationByIdQueryValidation : AbstractValidator<GetinKindDonationByIdQuery>
    {
        public GetinKindDonationByIdQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ik => ik.Id)
                .NotNull().WithMessage(ik => $"{nameof(ik.Id)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(ik => $"{nameof(ik.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ik => $"{nameof(ik.Id)} can not less than 36 Characters.");

        }
    }
}
