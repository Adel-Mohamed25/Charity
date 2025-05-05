using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Commands.MakeMessageIsRead
{
    public record MakeMessageIsReadCommand(string MessageId) : IRequest<Response<string>>;
}
