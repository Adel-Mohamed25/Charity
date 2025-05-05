using Charity.Contracts.ServicesAbstraction;
using Charity.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Charity.Services.ServicesImplementation
{
    public class NotificationServices : INotificationServices
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationServices(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendNotificationToAllAsync(object message, CancellationToken cancellationToken)
        => _hubContext.Clients.All.SendAsync("ReceiveNotification", message, cancellationToken);

        public Task SendNotificationToAllExceptAsync(IEnumerable<string> excludedConnectionIds, object message, CancellationToken cancellationToken)
            => _hubContext.Clients.AllExcept(excludedConnectionIds).SendAsync("ReceiveNotification", message, cancellationToken);

        public Task SendNotificationToGroupAsync(string group, object message, CancellationToken cancellationToken)
            => _hubContext.Clients.Group(group).SendAsync("ReceiveNotification", message, cancellationToken);

        public Task SendNotificationToGroupExceptAsync(string group, IEnumerable<string> excludedConnectionIds, object message, CancellationToken cancellationToken)
            => _hubContext.Clients.GroupExcept(group, excludedConnectionIds).SendAsync("ReceiveNotification", message, cancellationToken);

        public Task SendNotificationToGroupsAsync(IEnumerable<string> groups, object message, CancellationToken cancellationToken)
            => _hubContext.Clients.Groups(groups).SendAsync("ReceiveNotification", message, cancellationToken);

        public Task SendNotificationToUserAsync(string userId, object message, CancellationToken cancellationToken)
            => _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message, cancellationToken);

        public Task SendNotificationToUsersAsync(IEnumerable<string> userIds, object message, CancellationToken cancellationToken)
            => _hubContext.Clients.Users(userIds).SendAsync("ReceiveNotification", message, cancellationToken);
    }
}
