using Charity.Contracts.Repositories;
using FluentValidation;

namespace Charity.Application.Features.V1.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {

            RuleFor(u => u.UpdateUser.Id)
               .NotEmpty().WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be not empty.")
               .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be not null.")
               .MaximumLength(36).WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be exceed 36 characters.")
               .MinimumLength(36).WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be less than 36 characters.");


            RuleFor(u => u.UpdateUser.FirstName)
                .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.FirstName)} can not be null.")
                .NotEmpty().WithMessage(u => $"{nameof(u.UpdateUser.FirstName)} can not be null.")
                .MaximumLength(100).WithMessage(u => $"{nameof(u.UpdateUser.FirstName)} can not exceed 100 characters.")
                .MinimumLength(3).WithMessage(u => $"{nameof(u.UpdateUser.FirstName)} can not less than 3 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(u => $"{nameof(u.UpdateUser.FirstName)} contains invalid characters.");

            RuleFor(u => u.UpdateUser.LastName)
                .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.LastName)} can not be null.")
                .NotEmpty().WithMessage(u => $"{nameof(u.UpdateUser.LastName)} can not be null.")
                .MaximumLength(100).WithMessage(u => $"{nameof(u.UpdateUser.LastName)} can not exceed 100 characters.")
                .MinimumLength(3).WithMessage(u => $"{nameof(u.UpdateUser.LastName)} can not less than 3 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(u => $"{nameof(u.UpdateUser.LastName)} contains invalid characters.");

            RuleFor(u => u.UpdateUser.Email)
                .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.Email)} is required.")
                .NotEmpty().WithMessage(u => $"{nameof(u.UpdateUser.Email)} Can not be empty.")
                .EmailAddress().WithMessage(u => $"Invalid {nameof(u.UpdateUser.Email)} format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage(u => $"Invalid {nameof(u.UpdateUser.Email)} format. Please enter a valid email address.")
                .MustAsync(IsEmailUnique).WithMessage(r => $"{nameof(r.UpdateUser.Email)} already exists.");

            RuleFor(u => u.UpdateUser.Address)
                .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.Address)} can not be null.")
                .NotEmpty().WithMessage(u => $"{nameof(u.UpdateUser.Address)} can not be empty.")
                .MinimumLength(3).WithMessage(u => $"{nameof(u.UpdateUser.Address)} can not less than 5 characters.")
                .MaximumLength(200).WithMessage(u => $"{nameof(u.UpdateUser.Address)} can not exceed 200 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(u => $"{nameof(u.UpdateUser.Address)} contains invalid characters.");


            RuleFor(u => u.UpdateUser.PhoneNumber)
              .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.PhoneNumber)} can not be null.")
              .NotEmpty().WithMessage(u => $"{nameof(u.UpdateUser.PhoneNumber)} can not be empty.")
              .Matches("^(0|٠)1[0-9٠-٩]{9}$").WithMessage(u => $"{nameof(u.UpdateUser.PhoneNumber)} must start with 01 and contain exactly 11 digits. Letters and special characters are not allowed.");


            RuleFor(u => u.UpdateUser.DateOfBirth)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(u => $"{nameof(u.UpdateUser.DateOfBirth)} cannot be in the future.");


            RuleFor(r => r.UpdateUser.Gender)
              .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.Gender)} can not be null.")
              .IsInEnum().WithMessage(u => $"Invalid {nameof(u.UpdateUser.Gender)} value. Allowed values are Male (0), Female (1).");

        }

        private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.CharityUsers.IsExistAsync(u => u.Email!.Equals(email), cancellationToken);
        }

    }
}
