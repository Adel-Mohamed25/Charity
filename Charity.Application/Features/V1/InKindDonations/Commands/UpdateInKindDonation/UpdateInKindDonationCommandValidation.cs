using FluentValidation;

namespace Charity.Application.Features.V1.InKindDonations.Commands.UpdateInKindDonation
{
    public class UpdateInKindDonationCommandValidation : AbstractValidator<UpdateInKindDonationCommand>
    {
        public UpdateInKindDonationCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ik => ik.UpdateInKindDonation.Name)
                .MaximumLength(200).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Name)} can not exceed 200 Characters.")
                .MinimumLength(3).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Name)} can not less than 3 Characters.");


            RuleFor(ik => ik.UpdateInKindDonation.Description)
                .NotNull().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Description)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Description)} can not be empty.")
                .MaximumLength(500).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Description)} can not exceed 500 Characters.");

            RuleFor(ik => ik.UpdateInKindDonation.ItemType)
                .NotNull().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.ItemType)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.ItemType)} can not be empty.")
                .IsInEnum().WithMessage(ik => $"Invalid {nameof(ik.UpdateInKindDonation.ItemType)} value. Allowed values are (1) Clothes, (2) Food, (3) Medical Supplies, (4) Other.");

            RuleFor(ik => ik.UpdateInKindDonation.DonationStatus)
                .NotNull().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.DonationStatus)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.DonationStatus)} can not be empty.")
                .IsInEnum().WithMessage(ik => $"Invalid {nameof(ik.UpdateInKindDonation.DonationStatus)} value. Allowed values are (1) New, (2) UsedExcellentCondition, (3) UsedGoodCondition.");


            RuleFor(ik => ik.UpdateInKindDonation.Quantity)
                .NotNull().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Quantity)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Quantity)} can not be empty.")
                .GreaterThan(0).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.Quantity)} can not be less than 0 .");


            RuleFor(ik => ik.UpdateInKindDonation.Images)
                .Must(files => files == null || files.Count <= 5)
                .WithMessage("You can only attach up to 5 files.")
                .Must(files => files == null || files.All(file => file.Length <= 2 * 1024 * 1024))
                .WithMessage("Each file must be 2MB or smaller.");


            RuleFor(ik => ik.UpdateInKindDonation.DonorId)
                .NotNull().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.DonorId)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.DonorId)} can not be empty.")
                .MaximumLength(36).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.DonorId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.DonorId)} can not less than 36 Characters.");

            RuleFor(ik => ik.UpdateInKindDonation.ProjectId)
                .MaximumLength(36).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.ProjectId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ik => $"{nameof(ik.UpdateInKindDonation.ProjectId)} can not less than 36 Characters.");


        }
    }
}
