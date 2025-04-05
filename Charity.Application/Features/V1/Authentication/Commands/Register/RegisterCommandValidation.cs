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
                .NotNull().WithMessage("FirstName can not be null.")
                .NotEmpty().WithMessage("FirstName can not be null.")
                .MaximumLength(100).WithMessage("FirstName can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("FirstName can not less than 3 characters.")
                .Matches(@"^[a-zA-Z\s,.-]+$").WithMessage("Address contains invalid characters.");

            RuleFor(r => r.CreateUser.LastName)
                .NotNull().WithMessage("LastName can not be null.")
                .NotEmpty().WithMessage("LastName can not be null.")
                .MaximumLength(100).WithMessage("LastName can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("LastName can not less than 3 characters.")
                .Matches(@"^[a-zA-Z\s,.-]+$").WithMessage("Address contains invalid characters.");

            RuleFor(r => r.CreateUser.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can not be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format. Please enter a valid email address.")
                .MustAsync(IsEmailUnique).WithMessage("Email already exists.");

            RuleFor(r => r.CreateUser.Address)
                .NotNull().WithMessage("Address can not be null.")
                .NotEmpty().WithMessage("Address can not be empty.")
                .MinimumLength(5).WithMessage("Address can not less than 5 characters.")
                .MaximumLength(200).WithMessage("Address can not exceed 200 characters.")
                .Matches(@"^[a-zA-Z0-9\s,.-]+$").WithMessage("Address contains invalid characters.");


            RuleFor(r => r.CreateUser.PhoneNumber)
              .NotNull().WithMessage("PhoneNumber can not be null.")
              .NotEmpty().WithMessage("PhoneNumber can not be empty.")
              .MinimumLength(11).WithMessage("PhoneNumber can not less than 11 characters.")
              .MaximumLength(11).WithMessage("PhoneNumber can not exceed 11 characters.");


            RuleFor(r => r.CreateUser.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("DateOfBirth cannot be in the future.");


            RuleFor(r => r.CreateUser.Gender)
              .NotNull().WithMessage("Gender can not be null.")
              .NotEmpty().WithMessage("Gender can not be empty.")
              .IsInEnum().WithMessage("Invalid gender value. Allowed values are Male (1), Female (2).");

            RuleFor(r => r.CreateUser.UserType)
              .NotNull().WithMessage("UserType can not be null.")
              .NotEmpty().WithMessage("UserType can not be empty.")
              .IsInEnum().WithMessage("Invalid UserType value. Allowed values are Beneficiary (1), Volunteer (2), Donor (3), Admin (4).");



            RuleFor(r => r.CreateUser.Password)
                .NotNull().WithMessage("Password can not be null.")
                .NotEmpty().WithMessage("Password can not be empty.")
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one digit.")
                .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.")
                .Must(HaveUniqueCharacters).WithMessage("Password must have at least 8 unique characters.");

            RuleFor(rp => rp.CreateUser.ConfirmPassword)
                 .NotNull().WithMessage("Confirm password can not be null.")
                 .NotEmpty().WithMessage("Confirm password can not be empty")
                 .Equal(rp => rp.CreateUser.Password).WithMessage("Password and confirm password do not match.");

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
