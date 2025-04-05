using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetPaginatedinKindDonations
{
    public record GetPaginatedinKindDonationsQuery(PaginationModel Pagination)
        : IRequest<ResponsePagination<IEnumerable<InKindDonationModel>>>;
}
