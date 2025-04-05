using FluentValidation;

namespace Charity.Application.Features.V1.User.Queries.GetUserById
{
    public class GetUserByIdQueryValidation : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(gu => gu.Id)
               .NotEmpty().WithMessage(gu => $"{nameof(gu.Id)} can not be not empty.")
               .NotNull().WithMessage(gu => $"{nameof(gu.Id)} can not be not null.")
               .MaximumLength(36).WithMessage(gu => $"{nameof(gu.Id)} can not be exceed 36 characters.")
               .MinimumLength(36).WithMessage(gu => $"{nameof(gu.Id)} can not be less than 36 characters.");
        }
    }
}
