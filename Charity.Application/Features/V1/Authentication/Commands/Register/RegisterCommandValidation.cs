using Charity.Contracts.Repositories;
using FluentValidation;

namespace Charity.Application.Features.V1.Authentication.Commands.Register
{
    public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandValidation(IUnitOfWork unitOfWork)
        {
            ApplyValidationRules();
            _unitOfWork = unitOfWork;
        }

        private void ApplyValidationRules()
        {
            RuleFor(r => r.CreateUser.FirstName)
                .NotNull().WithMessage(r => $"{nameof(r.CreateUser.FirstName)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.FirstName)} can not be null.")
                .MaximumLength(100).WithMessage(r => $"{nameof(r.CreateUser.FirstName)} can not exceed 100 characters.")
                .MinimumLength(3).WithMessage(r => $"{nameof(r.CreateUser.FirstName)} can not less than 3 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(r => $"{nameof(r.CreateUser.FirstName)} contains invalid characters.");

            RuleFor(r => r.CreateUser.LastName)
                .NotNull().WithMessage(r => $"{nameof(r.CreateUser.LastName)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.LastName)} can not be null.")
                .MaximumLength(100).WithMessage(r => $"{nameof(r.CreateUser.LastName)} can not exceed 100 characters.")
                .MinimumLength(3).WithMessage(r => $"{nameof(r.CreateUser.LastName)} can not less than 3 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(r => $"{nameof(r.CreateUser.LastName)} contains invalid characters.");

            RuleFor(r => r.CreateUser.Email)
                .NotNull().WithMessage(r => $"{nameof(r.CreateUser.Email)} is required.")
                .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.Email)} Can not be empty.")
                .EmailAddress().WithMessage(r => $"Invalid{nameof(r.CreateUser.Email)} format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage(r => $"Invalid {nameof(r.CreateUser.Email)} format. Please enter a valid email address.")
                .MustAsync(IsEmailUnique).WithMessage(r => $"{nameof(r.CreateUser.Email)} already exists.");

            RuleFor(r => r.CreateUser.Address)
                .NotNull().WithMessage(r => $"{nameof(r.CreateUser.Address)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.Address)} can not be empty.")
                .MinimumLength(3).WithMessage(r => $"{nameof(r.CreateUser.Address)} can not less than 5 characters.")
                .MaximumLength(200).WithMessage(r => $"{nameof(r.CreateUser.Address)} can not exceed 200 characters.")
                .Matches(@"^[\p{IsArabic}a-zA-Z0-9\s,.\-]+$").WithMessage(r => $"{nameof(r.CreateUser.Address)} contains invalid characters.");


            RuleFor(r => r.CreateUser.PhoneNumber)
              .NotNull().WithMessage(r => $"{nameof(r.CreateUser.PhoneNumber)} can not be null.")
              .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.PhoneNumber)} can not be empty.")
              .Matches("^(0|٠)1[0-9٠-٩]{9}$").WithMessage(r => $"{nameof(r.CreateUser.PhoneNumber)} must start with 01 and contain exactly 11 digits. Letters and special characters are not allowed.");


            RuleFor(r => r.CreateUser.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(r => $"{nameof(r.CreateUser.DateOfBirth)} cannot be in the future.");


            RuleFor(r => r.CreateUser.Gender)
              .NotNull().WithMessage(u => $"{nameof(u.CreateUser.Gender)} can not be null.")
              .IsInEnum().WithMessage(u => $"Invalid {nameof(u.CreateUser.Gender)} value. Allowed values are Male (0), Female (1).");


            RuleFor(r => r.CreateUser.Password)
                .NotNull().WithMessage(r => $"{nameof(r.CreateUser.Password)} can not be null.")
                .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.Password)} can not be empty.")
                .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.Password)} is required.")
                .MinimumLength(8).WithMessage(r => $"{nameof(r.CreateUser.Password)} must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage(r => $"{nameof(r.CreateUser.Password)} must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage(r => $"{nameof(r.CreateUser.Password)} must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage(r => $"{nameof(r.CreateUser.Password)} must contain at least one digit.")
                .Matches(@"[^a-zA-Z0-9]").WithMessage(r => $"{nameof(r.CreateUser.Password)} must contain at least one non-alphanumeric character.")
                .Must(HaveUniqueCharacters).WithMessage(r => $"{nameof(r.CreateUser.Password)} must have at least 8 unique characters.");

            RuleFor(rp => rp.CreateUser.ConfirmPassword)
                 .NotNull().WithMessage(r => $"{nameof(r.CreateUser.ConfirmPassword)} can not be null.")
                 .NotEmpty().WithMessage(r => $"{nameof(r.CreateUser.ConfirmPassword)} can not be empty")
                 .Equal(rp => rp.CreateUser.Password).WithMessage(r => $"{nameof(r.CreateUser.Password)} and {nameof(r.CreateUser.ConfirmPassword)} do not match.");

        }

        private bool HaveUniqueCharacters(string password)
        {
            return password.Distinct().Count() >= 8;
        }

        private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.CharityUsers.IsExistAsync(u => u.Email!.Equals(email), cancellationToken);
        }

    }
}
