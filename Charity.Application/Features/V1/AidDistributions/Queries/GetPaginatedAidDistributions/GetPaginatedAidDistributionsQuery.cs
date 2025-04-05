using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetPaginatedAidDistributions
{
    public record GetPaginatedAidDistributionsQuery(PaginationModel Pagination)
        : IRequest<Response<IEnumerable<AidDistributionModel>>>;
}
