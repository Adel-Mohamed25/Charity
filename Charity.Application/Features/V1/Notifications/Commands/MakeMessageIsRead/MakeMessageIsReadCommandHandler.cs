using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Commands.MakeMessageIsRead
{
    public class MakeMessageIsReadCommandHandler :
        IRequestHandler<MakeMessageIsReadCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MakeMessageIsReadCommandHandler> _logger;

        public MakeMessageIsReadCommandHandler(IUnitOfWork unitOfWork,
            ILogger<MakeMessageIsReadCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(MakeMessageIsReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _unitOfWork.Notifications.GetByAsync(n => n.Id.Equals(request.MessageId),
                    cancellationToken: cancellationToken);

                if (message is null)
                    return ResponseHandler.NotFound<string>(message: "Message not found.");

                message.IsRead = true;
                message.ModifiedDate = DateTime.Now;
                await _unitOfWork.Notifications.UpdateAsync(message, cancellationToken: cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
                return ResponseHandler.Success<string>(message: "The message has been read successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during make this message is read.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
