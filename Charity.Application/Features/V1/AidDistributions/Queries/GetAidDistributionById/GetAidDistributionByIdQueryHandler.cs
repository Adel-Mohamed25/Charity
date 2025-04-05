using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetAidDistributionById
{
    public class GetAidDistributionByIdQueryHandler :
        IRequestHandler<GetAidDistributionByIdQuery,
            Response<AidDistributionModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAidDistributionByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAidDistributionByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAidDistributionByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<AidDistributionModel>> Handle(GetAidDistributionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var aidDistribution = await _unitOfWork.AidDistributions.GetByAsync(u => u.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (aidDistribution is null)
                    return ResponseHandler.NotFound<AidDistributionModel>(message: "AidDistribution not found.");

                var result = _mapper.Map<AidDistributionModel>(aidDistribution);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving aidDistribution data.");
                return ResponseHandler.BadRequest<AidDistributionModel>(errors: ex.Message);
            }
        }
    }
}
