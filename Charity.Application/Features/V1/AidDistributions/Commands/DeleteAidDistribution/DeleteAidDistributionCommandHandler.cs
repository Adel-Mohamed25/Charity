using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AidDistributions.Commands.DeleteAidDistribution
{
    public class DeleteAidDistributionCommandHandler :
        IRequestHandler<DeleteAidDistributionCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteAidDistributionCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteAidDistributionCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteAidDistributionCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteAidDistributionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aidDistribution = await _unitOfWork.AidDistributions.GetByAsync(p => p.Id.Equals(request.Id), cancellationToken: cancellationToken);
                if (aidDistribution is null)
                    return ResponseHandler.NotFound<string>(message: "AidDistribution not found.");

                await _unitOfWork.AidDistributions.DeleteAsync(aidDistribution, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The aidDistribution has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete aidDistribution.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
