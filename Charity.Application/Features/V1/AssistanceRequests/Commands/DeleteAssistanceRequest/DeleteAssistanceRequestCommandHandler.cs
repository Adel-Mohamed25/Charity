using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.DeleteAssistanceRequest
{
    public class DeleteAssistanceRequestCommandHandler
        : IRequestHandler<DeleteAssistanceRequestCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteAssistanceRequestCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteAssistanceRequestCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteAssistanceRequestCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(DeleteAssistanceRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var assistanceRequest = await _unitOfWork.AssistanceRequests.GetByAsync(p => p.Id.Equals(request.Id), cancellationToken: cancellationToken);
                if (assistanceRequest is null)
                    return ResponseHandler.NotFound<string>(message: "Assistance Request not found.");

                await _unitOfWork.AssistanceRequests.DeleteAsync(assistanceRequest, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The assistance request has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete assistance request.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
