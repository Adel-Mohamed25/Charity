using FluentValidation;

namespace Charity.Application.Features.V1.AidDistributions.Commands.DeleteAidDistribution
{
    public class DeleteAidDistributionCommandValidation : AbstractValidator<DeleteAidDistributionCommand>
    {
        public DeleteAidDistributionCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ad => ad.Id)
                .NotNull().WithMessage(ad => $"{nameof(ad.Id)} can not be null.")
                .NotEmpty().WithMessage(ad => $"{nameof(ad.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(ad => $"{nameof(ad.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ad => $"{nameof(ad.Id)} can not less than 36 Characters.");
        }
    }
}
