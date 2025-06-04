using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetAllAssistanceRequestsById
{
    public record GetAllAssistanceRequestsByIdQuery(string BeneficiaryId) : IRequest<Response<IEnumerable<AssistanceRequestModel>>>;
}
