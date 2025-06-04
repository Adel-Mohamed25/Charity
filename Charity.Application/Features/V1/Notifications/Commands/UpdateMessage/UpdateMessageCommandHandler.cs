using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Commands.UpdateMessage
{
    public class UpdateMessageCommandHandler
        : IRequestHandler<UpdateMessageCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateMessageCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateMessageCommandHandler(IUnitOfWork unitOfWork,
            ILogger<UpdateMessageCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _unitOfWork.Notifications.GetByAsync(n => n.Id.Equals(request.Notification.Id),
                    cancellationToken: cancellationToken);

                if (notification == null)
                    return ResponseHandler.NotFound<string>(message: "Notification not found.");

                _mapper.Map(request.Notification, notification);
                await _unitOfWork.Notifications.UpdateAsync(notification, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseHandler.NoContent<string>(message: "The notification has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update notification.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
