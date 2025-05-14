using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetAssistanceRequestById
{
    public class GetAssistanceRequestByIdQueryHandler
        : IRequestHandler<GetAssistanceRequestByIdQuery, Response<AssistanceRequestModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAssistanceRequestByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAssistanceRequestByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAssistanceRequestByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<AssistanceRequestModel>> Handle(GetAssistanceRequestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var assistanceRequest = await _unitOfWork.AssistanceRequests.GetByAsync(ar => ar.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (assistanceRequest is null)
                    return ResponseHandler.NotFound<AssistanceRequestModel>(message: "Assistance request not found.");

                var result = _mapper.Map<AssistanceRequestModel>(assistanceRequest);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving assistance request data.");
                return ResponseHandler.BadRequest<AssistanceRequestModel>(errors: ex.Message);
            }
        }
    }
}
