using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.AidDistribution;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AidDistributions.Queries.GetPaginatedAidDistributions
{
    public class GetPaginatedAidDistributionsQueryHandler :
        IRequestHandler<GetPaginatedAidDistributionsQuery, Response<IEnumerable<AidDistributionModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedAidDistributionsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedAidDistributionsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedAidDistributionsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AidDistributionModel>>> Handle(GetPaginatedAidDistributionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var aidDistributions = await _unitOfWork.AidDistributions.GetAllAsync(orderBy: p => p.Id,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!aidDistributions.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<AidDistributionModel>>(message: "AidDistributions not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.AidDistributions.CountAsync(cancellationToken: cancellationToken));

                var data = await aidDistributions.ProjectTo<AidDistributionModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.AidDistributions.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving aidDistributions data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<AidDistributionModel>>(errors: ex.Message);
            }
        }
    }
}
