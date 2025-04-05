using FluentValidation;

namespace Charity.Application.Features.V1.Project.Queries.GetProjectById
{
    public class GetProjectByIdQueryValidation : AbstractValidator<GetProjectByIdQuery>
    {
        public GetProjectByIdQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(cp => cp.Id)
                .NotNull().WithMessage(cp => $"{nameof(cp.Id)} can not be null.")
                .NotEmpty().WithMessage(cp => $"{nameof(cp.Id)} can not be empty.")
                .MaximumLength(36).WithMessage(cp => $"{nameof(cp.Id)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(cp => $"{nameof(cp.Id)} can not less than 36 Characters.");

        }
    }
}
