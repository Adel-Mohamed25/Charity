using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetAssistanceRequestById
{
    public record GetAssistanceRequestByIdQuery(string Id) : IRequest<Response<AssistanceRequestModel>>;
}
