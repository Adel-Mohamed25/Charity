using FluentValidation;

namespace Charity.Application.Features.V1.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {

            RuleFor(u => u.Id)
               .NotEmpty().WithMessage(u => $"{nameof(u.Id)} can not be not empty.")
               .NotNull().WithMessage(u => $"{nameof(u.Id)} can not be not null.")
               .MaximumLength(36).WithMessage(u => $"{nameof(u.Id)} can not be exceed 36 characters.")
               .MinimumLength(36).WithMessage(u => $"{nameof(u.Id)} can not be less than 36 characters.");

            RuleFor(u => u.UpdateUser.Id)
              .NotEmpty().WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be not empty.")
              .NotNull().WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be not null.")
              .MaximumLength(36).WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be exceed 36 characters.")
              .MinimumLength(36).WithMessage(u => $"{nameof(u.UpdateUser.Id)} can not be less than 36 characters.")
              .Equal(u => u.Id).WithMessage(u => $"{nameof(u.Id)} and {nameof(u.UpdateUser.Id)} not matching.");


            RuleFor(u => u.UpdateUser.FirstName)
                .NotNull().WithMessage("FirstName can not be null.")
                .NotEmpty().WithMessage("FirstName can not be null.")
                .MaximumLength(100).WithMessage("FirstName can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("FirstName can not less than 3 characters.")
                .Matches(@"^[a-zA-Z\s,.-]+$").WithMessage("Address contains invalid characters.");

            RuleFor(u => u.UpdateUser.LastName)
                .NotNull().WithMessage("LastName can not be null.")
                .NotEmpty().WithMessage("LastName can not be null.")
                .MaximumLength(100).WithMessage("LastName can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("LastName can not less than 3 characters.")
                .Matches(@"^[a-zA-Z\s,.-]+$").WithMessage("Address contains invalid characters.");

            RuleFor(u => u.UpdateUser.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can not be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format. Please enter a valid email address.");

            RuleFor(u => u.UpdateUser.Address)
                .NotNull().WithMessage("Address can not be null.")
                .NotEmpty().WithMessage("Address can not be empty.")
                .MinimumLength(5).WithMessage("Address can not less than 5 characters.")
                .MaximumLength(200).WithMessage("Address can not exceed 200 characters.")
                .Matches(@"^[a-zA-Z0-9\s,.-]+$").WithMessage("Address contains invalid characters.");


            RuleFor(u => u.UpdateUser.PhoneNumber)
              .NotNull().WithMessage("PhoneNumber can not be null.")
              .NotEmpty().WithMessage("PhoneNumber can not be empty.")
              .MinimumLength(11).WithMessage("PhoneNumber can not less than 11 characters.")
              .MaximumLength(11).WithMessage("PhoneNumber can not exceed 11 characters.");


            RuleFor(u => u.UpdateUser.DateOfBirth)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DateOfBirth cannot be in the future.");


            RuleFor(u => u.UpdateUser.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedDate cannot be in the future.");

            RuleFor(u => u.UpdateUser.Gender)
              .NotNull().WithMessage("Gender can not be null.")
              .NotEmpty().WithMessage("Gender can not be empty.")
              .IsInEnum().WithMessage("Invalid gender value. Allowed values are Male (1), Female (2).");

            RuleFor(u => u.UpdateUser.UserType)
              .NotNull().WithMessage("UserType can not be null.")
              .NotEmpty().WithMessage("UserType can not be empty.")
              .IsInEnum().WithMessage("Invalid UserType value. Allowed values are Beneficiary (1), Volunteer (2), Donor (3), Admin (4).");

        }


    }
}
