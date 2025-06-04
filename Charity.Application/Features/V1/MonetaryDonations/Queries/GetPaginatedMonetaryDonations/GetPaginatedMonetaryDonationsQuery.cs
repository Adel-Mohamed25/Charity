using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.MonetaryDonations.Queries.GetPaginatedMonetaryDonations
{
    public record GetPaginatedMonetaryDonationsQuery(PaginationModel Pagination)
        : IRequest<Response<IEnumerable<MonetaryDonationModel>>>;
}
