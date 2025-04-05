using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Project.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Response<ProjectModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProjectByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetProjectByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<ProjectModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetByAsync(u => u.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (project is null)
                    return ResponseHandler.NotFound<ProjectModel>(message: "Project not found.");

                var result = _mapper.Map<ProjectModel>(project);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving project data.");
                return ResponseHandler.BadRequest<ProjectModel>(errors: ex.Message);
            }
        }
    }
}
