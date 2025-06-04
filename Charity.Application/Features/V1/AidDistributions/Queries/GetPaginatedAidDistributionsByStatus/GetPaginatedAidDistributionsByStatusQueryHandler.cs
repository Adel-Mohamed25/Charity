using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetPaginatedAidDistributionsByStatus
{
    public class GetPaginatedAidDistributionsByStatusQueryHandler
        : IRequestHandler<GetPaginatedAidDistributionsByStatusQuery,
            Response<IEnumerable<AidDistributionModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedAidDistributionsByStatusQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedAidDistributionsByStatusQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedAidDistributionsByStatusQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AidDistributionModel>>> Handle(GetPaginatedAidDistributionsByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var AidDistributions = await _unitOfWork.AidDistributions.GetAllAsync(
                    firstFilter: a => a.Status.Equals(request.Status),
                    orderBy: a => a.Id,
                    paginationOn: true,
                    orderByDirection: request.Pagination.OrderByDirection,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    cancellationToken: cancellationToken);

                if (!AidDistributions.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<AidDistributionModel>>(message: "AidDistribution not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.AidDistributions.CountAsync(a => a.Status.Equals(request.Status),
                       cancellationToken));

                var data = await AidDistributions.ProjectTo<AidDistributionModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.AidDistributions.CountAsync(a => a.Status.Equals(request.Status),
                    cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving aidDistribution data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<AidDistributionModel>>(errors: ex.Message);
            }
        }
    }
}
