using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AidDistributions.Commands.CreateAidDistribution
{
    public class CreateAidDistributionCommandHandler : IRequestHandler<CreateAidDistributionCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateAidDistributionCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateAidDistributionCommandHandler(IUnitOfWork unitOfWork,
            ILogger<CreateAidDistributionCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(CreateAidDistributionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aidDistribution = _mapper.Map<AidDistribution>(request.AidDistributionModel);
                await _unitOfWork.AidDistributions.CreateAsync(aidDistribution, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Created<string>(message: "The aidDistribution has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create aidDistribution.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
