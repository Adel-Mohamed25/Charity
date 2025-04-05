using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetAidDistributionById
{
    public record GetAidDistributionByIdQuery(string Id) : IRequest<Response<AidDistributionModel>>;
}
