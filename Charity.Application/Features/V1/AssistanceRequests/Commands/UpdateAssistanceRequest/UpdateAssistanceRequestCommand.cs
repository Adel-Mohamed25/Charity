using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.UpdateAssistanceRequest
{
    public record UpdateAssistanceRequestCommand(UpdateAssistanceRequestModel AssistanceRequest) : IRequest<Response<string>>;
}
