using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.MonetaryDonations.Queries.GetPaginatedMonetaryDonationsByDonorId
{
    public record GetPaginatedMonetaryDonationsByDonorIdQuery(string DonorId, PaginationModel Pagination)
        : IRequest<Response<IEnumerable<MonetaryDonationModel>>>;
}
