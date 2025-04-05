using FluentValidation;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetPaginatedinKindDonations
{
    public class GetPaginatedinKindDonationsQueryValidation : AbstractValidator<GetPaginatedinKindDonationsQuery>
    {
        public GetPaginatedinKindDonationsQueryValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(ik => ik.Pagination.PageNumber)
                .NotNull().WithMessage(ik => $"{nameof(ik.Pagination.PageNumber)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.Pagination.PageNumber)} can not be empty.")
                .GreaterThan(0).WithMessage(ik => $"{nameof(ik.Pagination.PageNumber)} can not be less than or equal 0.");

            RuleFor(ik => ik.Pagination.PageSize)
                .NotNull().WithMessage(ik => $"{nameof(ik.Pagination.PageSize)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.Pagination.PageSize)} can not be empty.")
                .GreaterThan(0).WithMessage(ik => $"{nameof(ik.Pagination.PageSize)} can not be less than or equal 0.")
                .LessThanOrEqualTo(50).WithMessage("PageSize must not exceed 50.");


            RuleFor(ik => ik.Pagination.OrderByDirection)
                .NotNull().WithMessage(ik => $"{nameof(ik.Pagination.OrderByDirection)} can not be null.")
                .NotEmpty().WithMessage(ik => $"{nameof(ik.Pagination.OrderByDirection)} can not be empty.")
                .IsInEnum().WithMessage(ik => $"Invalid {nameof(ik.Pagination.OrderByDirection)} value. Allowed values are Ascending (1), Descending (2).");

        }
    }
}
