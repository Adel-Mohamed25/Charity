using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AidDistributions.Commands.DeleteAidDistribution
{
    public record DeleteAidDistributionCommand(string Id) : IRequest<Response<string>>;
}
