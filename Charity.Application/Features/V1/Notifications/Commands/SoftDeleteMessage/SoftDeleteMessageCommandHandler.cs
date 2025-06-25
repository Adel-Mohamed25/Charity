using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Commands.SoftDeleteMessage
{
    public class SoftDeleteMessageCommandHandler
        : IRequestHandler<SoftDeleteMessageCommand,
            Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SoftDeleteMessageCommandHandler> _logger;

        public SoftDeleteMessageCommandHandler(IUnitOfWork unitOfWork,
            ILogger<SoftDeleteMessageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(SoftDeleteMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _unitOfWork.Notifications.GetByAsync(n => n.Id.Equals(request.MessageId) && !n.IsDeleted,
                    cancellationToken: cancellationToken);

                if (notification == null)
                    return ResponseHandler.NotFound<string>(message: "Notification not found.");

                notification.IsDeleted = true;
                await _unitOfWork.Notifications.UpdateAsync(notification, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseHandler.NoContent<string>(message: "The notification has been successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during soft delete notification.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
