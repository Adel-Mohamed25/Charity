using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler
        : IRequestHandler<DeleteMessageCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteMessageCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteMessageCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteMessageCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _unitOfWork.Notifications.GetByAsync(n => n.Id.Equals(request.messageId),
                    cancellationToken: cancellationToken);

                if (notification == null)
                    return ResponseHandler.NotFound<string>(message: "Notification not found.");

                await _unitOfWork.Notifications.DeleteAsync(notification, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseHandler.NoContent<string>(message: "The notification has been successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete notification.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
