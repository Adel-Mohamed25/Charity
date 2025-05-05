using Charity.Models.Notification;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesByReceiveId
{
    public record GetAllMessagesByReceiveIdCommand(string ReceiveId) : IRequest<Response<IEnumerable<NotificationModel>>>;
}
