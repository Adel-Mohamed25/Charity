using FluentValidation;

namespace Charity.Application.Features.V1.Role.Queries.GetRoleById
{
    public class GetRoleByIdQueryValidation : AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(gr => gr.Id)
                .NotEmpty().WithMessage(gr => $"{nameof(gr.Id)} can not be not empty.")
                .NotNull().WithMessage(gr => $"{nameof(gr.Id)} can not be not null.")
                .MaximumLength(36).WithMessage(gr => $"{nameof(gr.Id)} can not be exceed 36 characters.")
                .MinimumLength(36).WithMessage(gr => $"{nameof(gr.Id)} can not be less than 36 characters.");
        }
    }
}
