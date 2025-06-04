using FluentValidation;

namespace Charity.Application.Features.V1.MonetaryDonations.Commands.CreatePaymentIntent
{
    public class CreatePaymentIntentCommandValidation : AbstractValidator<CreatePaymentIntentCommand>
    {
        public CreatePaymentIntentCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(p => p.PaymentModel.DonorId)
                .NotNull().WithMessage(p => $"{nameof(p.PaymentModel.DonorId)} can not be null.")
                .NotEmpty().WithMessage(p => $"{nameof(p.PaymentModel.DonorId)} can not be empty.")
                .MaximumLength(36).WithMessage(p => $"{nameof(p.PaymentModel.DonorId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(p => $"{nameof(p.PaymentModel.DonorId)} can not less than 36 Characters.");

            RuleFor(p => p.PaymentModel.ProjectId)
               .MaximumLength(36).WithMessage(p => $"{nameof(p.PaymentModel.ProjectId)} can not exceed 36 Characters.")
               .MinimumLength(36).WithMessage(p => $"{nameof(p.PaymentModel.ProjectId)} can not less than 36 Characters.");

            RuleFor(p => p.PaymentModel.Amount)
                .NotNull().WithMessage(p => $"{nameof(p.PaymentModel.Amount)} can not be null.")
                .NotEmpty().WithMessage(p => $"{nameof(p.PaymentModel.Amount)} can not be empty.")
                .GreaterThan(25).WithMessage(p => $"{nameof(p.PaymentModel.Amount)} greater than 25 EGP.");
        }
    }
}
