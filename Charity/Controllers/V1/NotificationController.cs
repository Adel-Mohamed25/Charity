using Charity.Api.Controllers.Common;
using Charity.Application.Features.V1.Notifications.Commands.DeleteMessage;
using Charity.Application.Features.V1.Notifications.Commands.MakeMessageIsRead;
using Charity.Application.Features.V1.Notifications.Commands.SendToAll;
using Charity.Application.Features.V1.Notifications.Commands.SendToUser;
using Charity.Application.Features.V1.Notifications.Commands.SoftDeleteMessage;
using Charity.Application.Features.V1.Notifications.Commands.UpdateMessage;
using Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesByReceiveId;
using Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesBySendId;
using Charity.Application.Features.V1.Notifications.Queries.GetCountMessagesById;
using Charity.Models.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class NotificationController : BaseApiController
    {
        [HttpPost("send-to-all")]
        public async Task<IActionResult> SendToAll([FromBody] CreateNotificationModel notificationModel)
        {
            return NewResult(await Mediator.Send(new SendToAllCommand(notificationModel)));
        }

        [HttpPost("send-to-user")]
        public async Task<IActionResult> SendToUser(CreateNotificationModel notificationModel)
        {
            return NewResult(await Mediator.Send(new SendToUserCommand(notificationModel)));
        }

        [HttpPost("MakeMessageIsRead")]
        public async Task<IActionResult> MakeMessageIsRead([FromQuery] string messageId)
        {
            return NewResult(await Mediator.Send(new MakeMessageIsReadCommand(messageId)));
        }

        [HttpGet("GetAllMessagesByReceiveId")]
        public async Task<IActionResult> GetAllMessagesByReceiveId([FromQuery] string receiveId)
        {
            return NewResult(await Mediator.Send(new GetAllMessagesByReceiveIdCommand(receiveId)));
        }

        [HttpGet("GetAllMessagesBySendId")]
        public async Task<IActionResult> GetAllMessagesBySendId([FromQuery] string sendId)
        {
            return NewResult(await Mediator.Send(new GetAllMessagesBySendIdCommand(sendId)));
        }

        [HttpGet("GetCountMessagesById")]
        public async Task<IActionResult> GetCountMessagesById([FromQuery] string receiveId)
        {
            return NewResult(await Mediator.Send(new GetCountMessagesByIdCommand(receiveId)));
        }

        [HttpPut("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateNotificationModel notificationModel)
        {
            return NewResult(await Mediator.Send(new UpdateMessageCommand(notificationModel)));
        }

        [HttpDelete("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage([FromQuery] string messageId)
        {
            return NewResult(await Mediator.Send(new DeleteMessageCommand(messageId)));
        }

        [HttpDelete("SoftDeleteMessage")]
        public async Task<IActionResult> SoftDeleteMessage([FromQuery] string messageId)
        {
            return NewResult(await Mediator.Send(new SoftDeleteMessageCommand(messageId)));
        }

        //[HttpPost("send-to-all-except")]
        //public async Task<IActionResult> SendToAllExcept([FromBody] NotificationExceptRequest request, CancellationToken cancellationToken)
        //{
        //    await _notificationServices.SendNotificationToAllExceptAsync(request.ExcludedConnectionIds, request.MethodName, request.Message, cancellationToken);
        //    return Ok("Notification sent to all except specified connections.");
        //}

        //[HttpPost("send-to-group")]
        //public async Task<IActionResult> SendToGroup(string group, string methodName, [FromBody] object message, CancellationToken cancellationToken)
        //{
        //    await _notificationServices.SendNotificationToGroupAsync(group, methodName, message, cancellationToken);
        //    return Ok($"Notification sent to group {group}.");
        //}

        //[HttpPost("send-to-group-except")]
        //public async Task<IActionResult> SendToGroupExcept([FromBody] GroupNotificationExceptRequest request, CancellationToken cancellationToken)
        //{
        //    await _notificationServices.SendNotificationToGroupExceptAsync(request.Group, request.ExcludedConnectionIds, request.MethodName, request.Message, cancellationToken);
        //    return Ok($"Notification sent to group {request.Group} except specified connections.");
        //}

        //[HttpPost("send-to-groups")]
        //public async Task<IActionResult> SendToGroups([FromBody] GroupsNotificationRequest request, CancellationToken cancellationToken)
        //{
        //    await _notificationServices.SendNotificationToGroupsAsync(request.Groups, request.MethodName, request.Message, cancellationToken);
        //    return Ok("Notification sent to specified groups.");
        //}


        //[HttpPost("send-to-users")]
        //public async Task<IActionResult> SendToUsers([FromBody] UsersNotificationRequest request, CancellationToken cancellationToken)
        //{
        //    await _notificationServices.SendNotificationToUsersAsync(request.UserIds, request.MethodName, request.Message, cancellationToken);
        //    return Ok("Notification sent to specified users.");
        //}
    }
}
