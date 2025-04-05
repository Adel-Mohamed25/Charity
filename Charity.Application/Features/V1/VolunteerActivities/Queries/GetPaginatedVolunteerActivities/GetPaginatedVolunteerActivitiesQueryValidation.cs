using FluentValidation;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetPaginatedVolunteerActivities
{
    public class GetPaginatedVolunteerActivitiesQueryValidation
        : AbstractValidator<GetPaginatedVolunteerActivitiesQuery>
    {
        public GetPaginatedVolunteerActivitiesQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(va => va.Pagination.PageNumber)
                .NotNull().WithMessage(va => $"{nameof(va.Pagination.PageNumber)} can not be null.")
                .NotEmpty().WithMessage(va => $"{nameof(va.Pagination.PageNumber)} can not be empty.")
                .GreaterThan(0).WithMessage(va => $"{nameof(va.Pagination.PageNumber)} can not be less than or equal 0.");

            RuleFor(va => va.Pagination.PageSize)
                .NotNull().WithMessage(va => $"{nameof(va.Pagination.PageSize)} can not be null.")
                .NotEmpty().WithMessage(va => $"{nameof(va.Pagination.PageSize)} can not be empty.")
                .GreaterThan(0).WithMessage(va => $"{nameof(va.Pagination.PageSize)} can not be less than or equal 0.")
                .LessThanOrEqualTo(50).WithMessage("PageSize must not exceed 50.");


            RuleFor(va => va.Pagination.OrderByDirection)
                .NotNull().WithMessage(va => $"{nameof(va.Pagination.OrderByDirection)} can not be null.")
                .NotEmpty().WithMessage(va => $"{nameof(va.Pagination.OrderByDirection)} can not be empty.")
                .IsInEnum().WithMessage(va => $"Invalid {nameof(va.Pagination.OrderByDirection)} value. Allowed values are Ascending (1), Descending (2).");

        }
    }
}
