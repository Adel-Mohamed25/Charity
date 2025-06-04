using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetActivitiesPaginatedByRequestStatus
{
    public class GetActivitiesPaginatedByRequestStatusQueryHandler
        : IRequestHandler<GetActivitiesPaginatedByRequestStatusQuery,
            Response<IEnumerable<VolunteerApplicationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivitiesPaginatedByRequestStatusQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetActivitiesPaginatedByRequestStatusQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetActivitiesPaginatedByRequestStatusQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<VolunteerApplicationModel>>> Handle(GetActivitiesPaginatedByRequestStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplications = await _unitOfWork.VolunteerApplications.GetAllAsync(
                    firstFilter: v => v.RequestStatus.Equals(request.RequestStatus) && v.VolunteerActivityId != null,
                    orderBy: a => a.Id,
                    paginationOn: true,
                    orderByDirection: request.Pagination.OrderByDirection,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    cancellationToken: cancellationToken);

                if (!volunteerApplications.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<VolunteerApplicationModel>>(message: "Volunteer applications for volunteer activities not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.VolunteerApplications.CountAsync(v => v.RequestStatus.Equals(request.RequestStatus)
                       && v.VolunteerActivityId != null,
                       cancellationToken));

                var data = await volunteerApplications.ProjectTo<VolunteerApplicationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.VolunteerApplications.CountAsync(v => v.RequestStatus.Equals(request.RequestStatus)
                    && v.VolunteerActivityId != null,
                    cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer applications for volunteer activities.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<VolunteerApplicationModel>>(errors: ex.Message);
            }
        }
    }
}
