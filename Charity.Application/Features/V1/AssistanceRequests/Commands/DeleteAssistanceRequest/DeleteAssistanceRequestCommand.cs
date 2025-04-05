using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.DeleteAssistanceRequest
{
    public record DeleteAssistanceRequestCommand(string Id) : IRequest<Response<string>>;
}
