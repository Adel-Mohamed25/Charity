using Charity.Models.Notification;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Commands.UpdateMessage
{
    public record UpdateMessageCommand(UpdateNotificationModel Notification) : IRequest<Response<string>>;
}
