using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Notifications.Commands.SendToAll
{
    public class SendToAllCommandHandler : IRequestHandler<SendToAllCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SendToAllCommandHandler> _logger;
        private readonly IMapper _mapper;

        public SendToAllCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<SendToAllCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(SendToAllCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfService.NotificationServices.SendNotificationToAllAsync(request.NotificationModel.Message, cancellationToken);
                var notification = _mapper.Map<Notification>(request.NotificationModel);
                await _unitOfWork.Notifications.CreateAsync(notification, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.NoContent<string>(message: "The message was sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during sent notification to all.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
