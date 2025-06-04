using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Project.Queries.GetPaginatedProjects
{
    public class GetPaginatedProjectsQueryHandler : IRequestHandler<GetPaginatedProjectsQuery, ResponsePagination<IEnumerable<ProjectModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedProjectsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedProjectsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedProjectsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponsePagination<IEnumerable<ProjectModel>>> Handle(GetPaginatedProjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var projects = await _unitOfWork.Projects.GetAllAsync(orderBy: p => p.Name,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!projects.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<ProjectModel>>(message: "Projects not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.Projects.CountAsync(cancellationToken: cancellationToken));

                var data = await projects.ProjectTo<ProjectModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.Projects.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving projects data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<ProjectModel>>(errors: ex.Message);
            }
        }
    }
}
