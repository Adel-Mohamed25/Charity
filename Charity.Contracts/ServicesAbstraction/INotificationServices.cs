namespace Charity.Contracts.ServicesAbstraction
{
    public interface INotificationServices
    {
        Task SendNotificationToUserAsync(string userId, object message, CancellationToken cancellationToken);
        Task SendNotificationToUsersAsync(IEnumerable<string> userIds, object message, CancellationToken cancellationToken);
        Task SendNotificationToGroupAsync(string group, object message, CancellationToken cancellationToken);
        Task SendNotificationToGroupsAsync(IEnumerable<string> groups, object message, CancellationToken cancellationToken);
        Task SendNotificationToAllAsync(object message, CancellationToken cancellationToken);
        Task SendNotificationToAllExceptAsync(IEnumerable<string> excludedConnectionIds, object message, CancellationToken cancellationToken);
        Task SendNotificationToGroupExceptAsync(string group, IEnumerable<string> excludedConnectionIds, object message, CancellationToken cancellationToken);
    }
}
