using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetAllAssistanceRequests
{
    public record GetAllAssistanceRequestsQuery() : IRequest<Response<IEnumerable<AssistanceRequestModel>>>;
}
