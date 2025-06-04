using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Queries.GetCountMessagesById
{
    public class GetCountMessagesByIdCommandHandler :
        IRequestHandler<GetCountMessagesByIdCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCountMessagesByIdCommandHandler> _logger;

        public GetCountMessagesByIdCommandHandler(IUnitOfWork unitOfWork,
            ILogger<GetCountMessagesByIdCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(GetCountMessagesByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _unitOfWork.CharityUsers.IsExistAsync(u => u.Id.Equals(request.ReceiveId)))
                    return ResponseHandler.NotFound<string>(message: "User not found.");

                var messagesNum = await _unitOfWork.Notifications.CountAsync(n => (n.ReceiverId!.Equals(request.ReceiveId) || n.ReceiverId == null)
                && !n.IsRead, cancellationToken);

                if (messagesNum is 0)
                    return ResponseHandler.NotFound<string>(message: "Not found any messages for this user to count.");

                return ResponseHandler.Success<string>(data: messagesNum.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during count messages for this user.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
