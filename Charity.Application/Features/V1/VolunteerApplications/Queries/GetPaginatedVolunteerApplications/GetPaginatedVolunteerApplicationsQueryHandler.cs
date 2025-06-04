using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetPaginatedVolunteerApplications
{
    public class GetPaginatedVolunteerApplicationsQueryHandler :
        IRequestHandler<GetPaginatedVolunteerApplicationsQuery, Response<IEnumerable<VolunteerApplicationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedVolunteerApplicationsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedVolunteerApplicationsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedVolunteerApplicationsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<VolunteerApplicationModel>>> Handle(GetPaginatedVolunteerApplicationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplications = await _unitOfWork.VolunteerApplications.GetAllAsync(orderBy: va => va.Id,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!volunteerApplications.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<VolunteerApplicationModel>>(message: "Volunteer applications not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.VolunteerApplications.CountAsync(cancellationToken: cancellationToken));

                var data = await volunteerApplications.ProjectTo<VolunteerApplicationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.VolunteerApplications.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer applications.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<VolunteerApplicationModel>>(errors: ex.Message);
            }
        }
    }
}
