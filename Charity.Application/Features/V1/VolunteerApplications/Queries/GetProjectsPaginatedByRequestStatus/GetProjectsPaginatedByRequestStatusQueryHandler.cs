using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetProjectsPaginatedByRequestStatus
{
    public class GetProjectsPaginatedByRequestStatusQueryHandler
        : IRequestHandler<GetProjectsPaginatedByRequestStatusQuery,
            Response<IEnumerable<VolunteerApplicationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProjectsPaginatedByRequestStatusQueryHandler> _logger;

        public GetProjectsPaginatedByRequestStatusQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetProjectsPaginatedByRequestStatusQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<IEnumerable<VolunteerApplicationModel>>> Handle(GetProjectsPaginatedByRequestStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplications = await _unitOfWork.VolunteerApplications.GetAllAsync(
                    firstFilter: v => v.RequestStatus.Equals(request.RequestStatus) && v.ProjectId != null,
                    orderBy: a => a.Id,
                    paginationOn: true,
                    orderByDirection: request.Pagination.OrderByDirection,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    cancellationToken: cancellationToken);

                if (!volunteerApplications.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<VolunteerApplicationModel>>(message: "Volunteer applications for projects not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.VolunteerApplications.CountAsync(v => v.RequestStatus.Equals(request.RequestStatus)
                       && v.ProjectId != null,
                       cancellationToken));

                var data = await volunteerApplications.ProjectTo<VolunteerApplicationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.VolunteerApplications.CountAsync(v => v.RequestStatus.Equals(request.RequestStatus)
                    && v.ProjectId != null,
                    cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer applications for projects.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<VolunteerApplicationModel>>(errors: ex.Message);
            }
        }
    }
}
