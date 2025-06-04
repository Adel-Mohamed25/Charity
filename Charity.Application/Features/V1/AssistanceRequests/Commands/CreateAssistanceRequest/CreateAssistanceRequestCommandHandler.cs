using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AssistanceRequests.Commands.CreateAssistanceRequest
{
    public class CreateAssistanceRequestCommandHandler
        : IRequestHandler<CreateAssistanceRequestCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateAssistanceRequestCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateAssistanceRequestCommandHandler(IUnitOfWork unitOfWork,
            ILogger<CreateAssistanceRequestCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateAssistanceRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var assistancerequest = _mapper.Map<AssistanceRequest>(request.CreateAssistance);
                await _unitOfWork.AssistanceRequests.CreateAsync(assistancerequest, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Created<string>(message: "The assistance request has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create assistance request.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
