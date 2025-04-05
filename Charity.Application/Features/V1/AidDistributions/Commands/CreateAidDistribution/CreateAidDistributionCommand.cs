using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AidDistributions.Commands.CreateAidDistribution
{
    public record CreateAidDistributionCommand(CreateAidDistributionModel AidDistributionModel)
        : IRequest<Response<string>>;
}
