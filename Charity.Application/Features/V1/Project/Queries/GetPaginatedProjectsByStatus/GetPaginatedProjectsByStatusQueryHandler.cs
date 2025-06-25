using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Project.Queries.GetPaginatedProjectsByStatus
{
    public class GetPaginatedProjectsByStatusQueryHandler
        : IRequestHandler<GetPaginatedProjectsByStatusQuery,
            Response<IEnumerable<ProjectModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedProjectsByStatusQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedProjectsByStatusQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedProjectsByStatusQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ProjectModel>>> Handle(GetPaginatedProjectsByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var AidDistributions = await _unitOfWork.Projects.GetAllAsync(
                    firstFilter: p => p.ProjectStatus.Equals(request.Status),
                    orderBy: p => p.Id,
                    paginationOn: true,
                    orderByDirection: request.Pagination.OrderByDirection,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    cancellationToken: cancellationToken);

                if (!AidDistributions.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<ProjectModel>>(message: "Projects not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.Projects.CountAsync(p => p.ProjectStatus.Equals(request.Status),
                       cancellationToken));

                var data = await AidDistributions.ProjectTo<ProjectModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.Projects.CountAsync(p => p.ProjectStatus.Equals(request.Status),
                    cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving projects data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<ProjectModel>>(errors: ex.Message);
            }
        }
    }
}
