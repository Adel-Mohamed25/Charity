using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.Notification;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesByReceiveId
{
    public class GetAllMessagesByReceiveIdCommandHandler :
        IRequestHandler<GetAllMessagesByReceiveIdCommand, Response<IEnumerable<NotificationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllMessagesByReceiveIdCommandHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllMessagesByReceiveIdCommandHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllMessagesByReceiveIdCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<NotificationModel>>> Handle(GetAllMessagesByReceiveIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _unitOfWork.CharityUsers.IsExistAsync(u => u.Id.Equals(request.ReceiveId)))
                    return ResponseHandler.NotFound<IEnumerable<NotificationModel>>(message: "User not found.");

                var messages = await _unitOfWork.Notifications.GetAllAsync(n => (n.ReceiverId!.Equals(request.ReceiveId)
                                                                  || n.ReceiverId == null) && !n.IsDeleted,
                                                                  orderBy: n => n.CreatedDate!,
                                                                  orderByDirection: OrderByDirection.Descending,
                                                                  cancellationToken: cancellationToken);

                if (!messages.Any())
                    return ResponseHandler.NotFound<IEnumerable<NotificationModel>>(message: "Not found any messages for this user.");

                var result = await messages.ProjectTo<NotificationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponseHandler.Success(data: result.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Messages for user data.");
                return ResponseHandler.BadRequest<IEnumerable<NotificationModel>>(errors: ex.Message);
            }
        }
    }
}
