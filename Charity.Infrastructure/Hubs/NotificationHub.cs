using Microsoft.AspNetCore.SignalR;

namespace Charity.Infrastructure.Hubs
{
    public class NotificationHub : Hub
    {
        /// <summary>
        /// Sends a message to all connected clients.
        /// </summary>
        /// <param name="methodName">The event name that the client listens for (e.g., "ReceiveNotification").</param>
        /// <param name="message">The message payload to send.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <awaits>An asynchronous task.</awaits>
        public async Task SendToAllAsync(string methodName, object message, CancellationToken cancellationToken)
        {
            await Clients.All.SendAsync(methodName, message, cancellationToken);
        }

        /// <summary>
        /// Sends a message to all connected clients except those with the specified connection IDs.
        /// </summary>
        /// <param name="methodName">The event name that the client listens for.</param>
        /// <param name="message">The message payload to send.</param>
        /// <param name="excludedConnectionIds">A collection of connection IDs to exclude.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <awaits>An asynchronous task.</awaits>
        public async Task SendToAllExceptAsync(string methodName, object message, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken)
        {
            await Clients.AllExcept(excludedConnectionIds).SendAsync(methodName, message, cancellationToken);
        }

        /// <summary>
        /// Sends a message to a specific group.
        /// </summary>
        /// <param name="methodName">The event name that the client listens for.</param>
        /// <param name="message">The message payload to send.</param>
        /// <param name="group">The target group name.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <awaits>An asynchronous task.</awaits>
        public async Task SendToGroupAsync(string methodName, object message, string group, CancellationToken cancellationToken)
        {
            await Clients.Group(group).SendAsync(methodName, message, cancellationToken);
        }

        /// <summary>
        /// Sends a message to a specific group while excluding certain connections.
        /// </summary>
        /// <param name="methodName">The event name that the client listens for.</param>
        /// <param name="message">The message payload to send.</param>
        /// <param name="group">The target group name.</param>
        /// <param name="excludedConnectionIds">A collection of connection IDs to exclude from the group.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <awaits>An asynchronous task.</awaits>
        public async Task SendToGroupExceptAsync(string methodName, object message, string group, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken)
        {
            await Clients.GroupExcept(group, excludedConnectionIds).SendAsync(methodName, message, cancellationToken);
        }

        /// <summary>
        /// Sends a message to multiple groups.
        /// </summary>
        /// <param name="methodName">The event name that the client listens for.</param>
        /// <param name="message">The message payload to send.</param>
        /// <param name="groupNames">A collection of target group names.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <awaits>An asynchronous task.</awaits>
        public async Task SendToGroupsAsync(string methodName, object message, IEnumerable<string> groupNames, CancellationToken cancellationToken)
        {
            await Clients.Groups(groupNames).SendAsync(methodName, message, cancellationToken);
        }

        /// <summary>
        /// Sends a message to a specific user.
        /// </summary>
        /// <param name="methodName">The event name that the client listens for.</param>
        /// <param name="message">The message payload to send.</param>
        /// <param name="userId">The user identifier for the target user.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <awaits>An asynchronous task.</awaits>
        public async Task SendToUserAsync(string methodName, object message, string userId, CancellationToken cancellationToken)
        {
            await Clients.User(userId).SendAsync(methodName, message, cancellationToken);
        }

        /// <summary>
        /// Sends a message to multiple specific users.
        /// </summary>
        /// <param name="methodName">The event name that the client listens for.</param>
        /// <param name="message">The message payload to send.</param>
        /// <param name="userIds">A collection of user identifiers for the target users.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <awaits>An asynchronous task.</awaits>
        public async Task SendToUsersAsync(string methodName, object message, IEnumerable<string> userIds, CancellationToken cancellationToken)
        {
            await Clients.Users(userIds).SendAsync(methodName, message, cancellationToken);
        }

    }
}
