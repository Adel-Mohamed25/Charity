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

namespace Charity.Application.Features.V1.Notifications.Queries.GetAllMessagesBySendId
{
    public class GetAllMessagesBySendIdCommandHandler :
        IRequestHandler<GetAllMessagesBySendIdCommand,
            Response<IEnumerable<NotificationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllMessagesBySendIdCommandHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllMessagesBySendIdCommandHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllMessagesBySendIdCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<NotificationModel>>> Handle(GetAllMessagesBySendIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _unitOfWork.CharityUsers.IsExistAsync(u => u.Id.Equals(request.SendId)))
                    return ResponseHandler.NotFound<IEnumerable<NotificationModel>>(message: "User not found.");

                var messages = await _unitOfWork.Notifications.GetAllAsync(n => n.SenderId!.Equals(request.SendId),
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
