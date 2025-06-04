using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Commands.DeleteMessage
{
    public record DeleteMessageCommand(string messageId) : IRequest<Response<string>>;
}
