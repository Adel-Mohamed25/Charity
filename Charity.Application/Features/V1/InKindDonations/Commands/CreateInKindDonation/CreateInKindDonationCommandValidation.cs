using FluentValidation;

namespace Charity.Application.Features.V1.InKindDonations.Commands.CreateInKindDonation
{
    public class CreateInKindDonationCommandValidation : AbstractValidator<CreateInKindDonationCommand>
    {
        public CreateInKindDonationCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ik => ik.InKindDonationModel.Name)
                .MaximumLength(200).WithMessage(ik => $"{nameof(ik.InKindDonationModel.Name)} can not exceed 200 Characters.")
                .MinimumLength(3).WithMessage(ik => $"{nameof(ik.InKindDonationModel.Name)} can not less than 3 Characters.");


            RuleFor(ik => ik.InKindDonationModel.Description)
                .NotNull().WithMessage(ik => $"{nameof(ik.InKindDonationModel.Description)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.InKindDonationModel.Description)} can not be empty.")
                .MaximumLength(500).WithMessage(cp => $"{nameof(cp.InKindDonationModel.Description)} can not exceed 500 Characters.");

            RuleFor(ik => ik.InKindDonationModel.ItemType)
                .NotNull().WithMessage(ik => $"{nameof(ik.InKindDonationModel.ItemType)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.InKindDonationModel.ItemType)} can not be empty.")
                .IsInEnum().WithMessage(ik => $"Invalid {nameof(ik.InKindDonationModel.ItemType)} value. Allowed values are (1) Clothes, (2) Food, (3) Medical Supplies, (4) Other.");


            RuleFor(ik => ik.InKindDonationModel.Quantity)
                .NotNull().WithMessage(ik => $"{nameof(ik.InKindDonationModel.Quantity)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.InKindDonationModel.Quantity)} can not be empty.")
                .GreaterThan(0).WithMessage(ik => $"{nameof(ik.InKindDonationModel.Quantity)} can not be less than 0 .");


            RuleFor(ik => ik.InKindDonationModel.Images)
                .Must(files => files == null || files.Count <= 5)
                .WithMessage("You can only attach up to 5 files.")
                .Must(files => files == null || files.All(file => file.Length <= 2 * 1024 * 1024))
                .WithMessage("Each file must be 2MB or smaller.");


            RuleFor(ik => ik.InKindDonationModel.DonorId)
                .NotNull().WithMessage(ik => $"{nameof(ik.InKindDonationModel.DonorId)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.InKindDonationModel.DonorId)} can not be empty.")
                .MaximumLength(36).WithMessage(ik => $"{nameof(ik.InKindDonationModel.DonorId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ik => $"{nameof(ik.InKindDonationModel.DonorId)} can not less than 36 Characters.");

            RuleFor(ik => ik.InKindDonationModel.ProjectId)
                .MaximumLength(36).WithMessage(ik => $"{nameof(ik.InKindDonationModel.ProjectId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(ik => $"{nameof(ik.InKindDonationModel.ProjectId)} can not less than 36 Characters.");

            RuleFor(ik => ik.InKindDonationModel.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(ik => $"Invalid {nameof(ik.InKindDonationModel.CreatedDate)} cannot be in the future.");

        }

    }
}
