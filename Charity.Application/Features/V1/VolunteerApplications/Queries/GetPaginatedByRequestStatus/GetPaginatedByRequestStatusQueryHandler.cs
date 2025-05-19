using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetPaginatedByRequestStatus
{
    public class GetPaginatedByRequestStatusQueryHandler
        : IRequestHandler<GetPaginatedByRequestStatusQuery,
            Response<IEnumerable<VolunteerApplicationModel>>>
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
        public async Task<Response<IEnumerable<VolunteerApplicationModel>>> Handle(GetPaginatedByRequestStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplications = await _unitOfWork.VolunteerApplications.GetAllAsync(
                    firstFilter: v => v.RequestStatus.Equals(request.RequestStatus),
                    orderBy: a => a.Id,
                    paginationOn: true,
                    orderByDirection: request.Pagination.OrderByDirection,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    cancellationToken: cancellationToken);

                if (!volunteerApplications.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<VolunteerApplicationModel>>(message: "Volunteer applications not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.VolunteerApplications.CountAsync(v => v.RequestStatus.Equals(request.RequestStatus),
                       cancellationToken));

                var data = _mapper.Map<IEnumerable<VolunteerApplicationModel>>(volunteerApplications);
                return ResponsePaginationHandler.Success(data: data,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.VolunteerApplications.CountAsync(v => v.RequestStatus.Equals(request.RequestStatus),
                    cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer applications  data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<VolunteerApplicationModel>>(errors: ex.Message);
            }
        }
    }
}
