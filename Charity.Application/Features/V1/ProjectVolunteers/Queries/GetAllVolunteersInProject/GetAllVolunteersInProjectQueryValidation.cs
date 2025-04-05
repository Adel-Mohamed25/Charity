using FluentValidation;

namespace Charity.Application.Features.V1.ProjectVolunteers.Queries.GetAllVolunteersInProject
{
    public class GetAllVolunteersInProjectQueryValidation : AbstractValidator<GetAllVolunteersInProjectQuery>
    {
        public GetAllVolunteersInProjectQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(gvp => gvp.ProjectId)
                .NotNull().WithMessage(gvp => $"{nameof(gvp.ProjectId)} can not be null.")
                .NotEmpty().WithMessage(gvp => $"{nameof(gvp.ProjectId)} can not be empty.")
                .MaximumLength(36).WithMessage(gvp => $"{nameof(gvp.ProjectId)} can not exceed 36 Characters.")
                .MinimumLength(36).WithMessage(gvp => $"{nameof(gvp.ProjectId)} can not less than 36 Characters.");

        }
    }
}
