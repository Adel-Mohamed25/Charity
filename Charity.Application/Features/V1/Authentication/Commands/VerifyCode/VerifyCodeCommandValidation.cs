using FluentValidation;

namespace Charity.Application.Features.V1.Authentication.Commands.VerifyCode
{
    public class VerifyCodeCommandValidation : AbstractValidator<VerifyCodeCommand>
    {
        public VerifyCodeCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(vc => vc.VerifyCode.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can not be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format. Please enter a valid email address.");

            RuleFor(vc => vc.VerifyCode.Code)
                .NotNull().WithMessage("Code can not be null.")
                .NotEmpty().WithMessage("Code can not be empty.")
                .MaximumLength(6).WithMessage("Code can not exceed 6 characters.")
                .MinimumLength(6).WithMessage("Code can not less than 6 characters.");
        }

    }
}
