using FluentValidation;

namespace Charity.Application.Features.V1.AidDistributions.Commands.CreateAidDistribution
{
    public class CreateAidDistributionCommandValidation : AbstractValidator<CreateAidDistributionCommand>
    {
        public CreateAidDistributionCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ad => ad.AidDistributionModel.BeneficiaryId)
                .NotNull().WithMessage(ad => $"{nameof(ad.AidDistributionModel.BeneficiaryId)} can not be null.")
                .NotEmpty().WithMessage(ad => $"{nameof(ad.AidDistributionModel.BeneficiaryId)} can not be empty.")
                .MaximumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.BeneficiaryId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.BeneficiaryId)} can not less than 36 Characters.");

            RuleFor(ad => ad.AidDistributionModel.VolunteerId)
                .NotNull().WithMessage(ad => $"{nameof(ad.AidDistributionModel.VolunteerId)} can not be null.")
                .NotEmpty().WithMessage(ad => $"{nameof(ad.AidDistributionModel.VolunteerId)} can not be empty.")
                .MaximumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.VolunteerId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.VolunteerId)} can not less than 36 Characters.");

            RuleFor(ad => ad.AidDistributionModel.InKindDonationId)
                .MaximumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.InKindDonationId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.InKindDonationId)} can not less than 36 Characters.");

            RuleFor(ad => ad.AidDistributionModel.MonetaryDonationId)
                .MaximumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.MonetaryDonationId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ad => $"{nameof(ad.AidDistributionModel.MonetaryDonationId)} can not less than 36 Characters.");

            RuleFor(ad => ad.AidDistributionModel.Description)
                .NotNull().WithMessage(ad => $"{nameof(ad.AidDistributionModel.Description)} can not be null.")
                .NotEmpty().WithMessage(ad => $"{nameof(ad.AidDistributionModel.Description)} can not be empty.")
                .MaximumLength(500).WithMessage(ad => $"{nameof(ad.AidDistributionModel.Description)} can not exceed 500 Characters.");
        }
    }
}
