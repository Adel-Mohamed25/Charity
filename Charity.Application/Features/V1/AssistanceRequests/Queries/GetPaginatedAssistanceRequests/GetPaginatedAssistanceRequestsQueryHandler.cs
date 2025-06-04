using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetPaginatedAssistanceRequests
{
    public class GetPaginatedAssistanceRequestsQueryHandler
        : IRequestHandler<GetPaginatedAssistanceRequestsQuery,
            ResponsePagination<IEnumerable<AssistanceRequestModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedAssistanceRequestsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedAssistanceRequestsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedAssistanceRequestsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponsePagination<IEnumerable<AssistanceRequestModel>>> Handle(GetPaginatedAssistanceRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var assistanceRequests = await _unitOfWork.AssistanceRequests.GetAllAsync(orderBy: p => p.Id,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!assistanceRequests.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<AssistanceRequestModel>>(message: "Assistance requests not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.AssistanceRequests.CountAsync(cancellationToken: cancellationToken));

                var data = await assistanceRequests
                    .ProjectTo<AssistanceRequestModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.AssistanceRequests.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving assistance requests data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<AssistanceRequestModel>>(errors: ex.Message);
            }
        }
    }
}
