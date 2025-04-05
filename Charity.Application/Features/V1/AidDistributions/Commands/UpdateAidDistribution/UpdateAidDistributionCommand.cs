using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AidDistributions.Commands.UpdateAidDistribution
{
    public record UpdateAidDistributionCommand(UpdateAidDistributionModel AidDistributionModel) : IRequest<Response<string>>;
}
