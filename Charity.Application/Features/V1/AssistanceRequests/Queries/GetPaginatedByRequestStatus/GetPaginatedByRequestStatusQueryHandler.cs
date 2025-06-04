using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetPaginatedByRequestStatus
{
    public class GetPaginatedByRequestStatusQueryHandler
        : IRequestHandler<GetPaginatedByRequestStatusQuery,
            Response<IEnumerable<AssistanceRequestModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaginatedByRequestStatusQueryHandler> _logger;

        public GetPaginatedByRequestStatusQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetPaginatedByRequestStatusQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<IEnumerable<AssistanceRequestModel>>> Handle(GetPaginatedByRequestStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var assistanceRequests = await _unitOfWork.AssistanceRequests.GetAllAsync(
                    firstFilter: a => a.RequestStatus.Equals(request.RequestStatus),
                    orderBy: a => a.Id,
                    paginationOn: true,
                    orderByDirection: request.Pagination.OrderByDirection,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    cancellationToken: cancellationToken);

                if (!assistanceRequests.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<AssistanceRequestModel>>(message: "Assistance requests not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.AssistanceRequests.CountAsync(a => a.RequestStatus.Equals(request.RequestStatus),
                       cancellationToken));

                var data = await assistanceRequests.ProjectTo<AssistanceRequestModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.AssistanceRequests.CountAsync(a => a.RequestStatus.Equals(request.RequestStatus),
                    cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving assistance requests data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<AssistanceRequestModel>>(errors: ex.Message);
            }
        }
    }
}
