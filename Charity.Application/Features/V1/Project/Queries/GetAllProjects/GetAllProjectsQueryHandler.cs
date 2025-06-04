using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Project.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, Response<IEnumerable<ProjectModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllProjectsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllProjectsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<ProjectModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var projects = await _unitOfWork.Projects.GetAllAsync(orderBy: p => p.Name!,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!projects.Any())
                    return ResponseHandler.NotFound<IEnumerable<ProjectModel>>(message: "Projects not found.");

                var result = await projects.ProjectTo<ProjectModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponseHandler.Success(data: result.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving projects data.");
                return ResponseHandler.BadRequest<IEnumerable<ProjectModel>>(errors: ex.Message);
            }
        }
    }
}
