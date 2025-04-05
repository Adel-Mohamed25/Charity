using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.CreateAssistanceRequest
{
    public record CreateAssistanceRequestCommand(CreateAssistanceRequestModel CreateAssistance) : IRequest<Response<string>>;
}
