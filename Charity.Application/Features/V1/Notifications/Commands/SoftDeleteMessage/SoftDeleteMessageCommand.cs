using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Commands.SoftDeleteMessage
{
    public record SoftDeleteMessageCommand(string MessageId) : IRequest<Response<string>>;
}
