using Charity.Models.Notification;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Commands.SendToUser
{
    public record SendToUserCommand(CreateNotificationModel NotificationModel) : IRequest<Response<string>>;
}
