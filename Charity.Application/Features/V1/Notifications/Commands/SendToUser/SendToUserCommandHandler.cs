using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Commands.SendToUser
{
    public class SendToUserCommandHandler : IRequestHandler<SendToUserCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<SendToUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public SendToUserCommandHandler(IUnitOfWork unitOfWork,
            IUnitOfService unitOfService,
            ILogger<SendToUserCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _unitOfService = unitOfService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(SendToUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfService.NotificationServices.SendNotificationToUserAsync(request.NotificationModel.ReceiverId, request.NotificationModel.Message, cancellationToken);
                var notification = _mapper.Map<Notification>(request.NotificationModel);
                await _unitOfWork.Notifications.CreateAsync(notification, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The message was sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during sent notification to user.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
