using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetAllAidDistributions
{
    public record GetAllAidDistributionsQuery() : IRequest<Response<IEnumerable<AidDistributionModel>>>;
}
