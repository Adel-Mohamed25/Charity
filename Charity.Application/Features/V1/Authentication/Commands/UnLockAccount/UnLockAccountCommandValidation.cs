using FluentValidation;

namespace Charity.Application.Features.V1.Authentication.Commands.UnLockAccount
{
    public class UnLockAccountCommandValidation : AbstractValidator<UnLockAccountCommand>
    {
        public UnLockAccountCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ula => ula.UserEmail.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can not be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format. Please enter a valid email address.");
        }
    }
}
