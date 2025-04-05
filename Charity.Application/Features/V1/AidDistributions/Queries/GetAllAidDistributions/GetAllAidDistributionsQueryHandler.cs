using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetAllAidDistributions
{
    public class GetAllAidDistributionsQueryHandler :
        IRequestHandler<GetAllAidDistributionsQuery,
            Response<IEnumerable<AidDistributionModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllAidDistributionsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllAidDistributionsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllAidDistributionsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<AidDistributionModel>>> Handle(GetAllAidDistributionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var aidDistributions = await _unitOfWork.AidDistributions.GetAllAsync(orderBy: p => p.Id,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!aidDistributions.Any())
                    return ResponseHandler.NotFound<IEnumerable<AidDistributionModel>>(message: "AidDistributions not found.");

                var result = _mapper.Map<IEnumerable<AidDistributionModel>>(aidDistributions);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving aidDistributions data.");
                return ResponseHandler.BadRequest<IEnumerable<AidDistributionModel>>(errors: ex.Message);
            }
        }
    }
}
