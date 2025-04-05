using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.UpdateAssistanceRequest
{
    public class UpdateAssistanceRequestCommandHandler
        : IRequestHandler<UpdateAssistanceRequestCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateAssistanceRequestCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateAssistanceRequestCommandHandler(IUnitOfWork unitOfWork,
            ILogger<UpdateAssistanceRequestCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateAssistanceRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var assistanceRequest = await _unitOfWork.AssistanceRequests.GetByAsync(p => p.Id.Equals(request.AssistanceRequest.Id),
                    cancellationToken: cancellationToken);

                if (assistanceRequest is null)
                    return ResponseHandler.NotFound<string>(message: "Assistance Request not found.");

                var result = _mapper.Map<AssistanceRequest>(request.AssistanceRequest);
                await _unitOfWork.AssistanceRequests.UpdateAsync(result, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The assistance request has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update assistance request.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
