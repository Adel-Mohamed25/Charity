using FluentValidation;

namespace Charity.Application.Features.V1.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandValidation : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(rt => rt.RefreshJwtModel.Jwt)
                .NotNull().WithMessage("Jwt can not be null.")
                .NotEmpty().WithMessage("Jwt can be empty.");

            RuleFor(rt => rt.RefreshJwtModel.RefreshJwt)
                .NotNull().WithMessage("RefreshJwt  can not be null.")
                .NotEmpty().WithMessage("JRefreshJwt can be empty.");
        }
    }
}
