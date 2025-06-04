using Charity.Domain.Enum;
using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetPaginatedAidDistributionsByStatus
{
    public record GetPaginatedAidDistributionsByStatusQuery(AidDistributionStatus Status, PaginationModel Pagination)
        : IRequest<Response<IEnumerable<AidDistributionModel>>>;
}
