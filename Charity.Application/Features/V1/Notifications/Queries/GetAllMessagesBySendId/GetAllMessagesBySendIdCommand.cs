using Charity.Models.Notification;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesBySendId
{
    public record GetAllMessagesBySendIdCommand(string SendId) : IRequest<Response<IEnumerable<NotificationModel>>>;
}
