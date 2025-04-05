using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AidDistributions.Commands.UpdateAidDistribution
{
    public class UpdateAidDistributionCommandHandler : IRequestHandler<UpdateAidDistributionCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateAidDistributionCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateAidDistributionCommandHandler(IUnitOfWork unitOfWork,
            ILogger<UpdateAidDistributionCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateAidDistributionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aidDistribution = await _unitOfWork.AidDistributions.GetByAsync(p => p.Id.Equals(request.AidDistributionModel.Id),
                    cancellationToken: cancellationToken);

                if (aidDistribution is null)
                    return ResponseHandler.NotFound<string>(message: "AidDistribution not found.");

                var result = _mapper.Map<AidDistribution>(request.AidDistributionModel);
                await _unitOfWork.AidDistributions.UpdateAsync(result, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The aidDistribution has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update aidDistribution.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
