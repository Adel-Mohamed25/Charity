using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.ProjectVolunteers.Queries.GetAllVolunteersInProject
{
    public class GetAllVolunteersInProjectQueryHandler :
        IRequestHandler<GetAllVolunteersInProjectQuery,
            Response<IEnumerable<VolunteerModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllVolunteersInProjectQuery> _logger;
        private readonly IMapper _mapper;

        public GetAllVolunteersInProjectQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllVolunteersInProjectQuery> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<VolunteerModel>>> Handle(GetAllVolunteersInProjectQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetByAsync(p => p.Id.Equals(request.ProjectId));
                if (project == null)
                    return ResponseHandler.NotFound<IEnumerable<VolunteerModel>>(message: "Project not found.");

                var projectVolunteers = await _unitOfWork.ProjectVolunteers
                    .GetAllAsync(pv => pv.ProjectId.Equals(request.ProjectId),
                    includes: "Volunteer");

                if (!projectVolunteers.Any())
                    return ResponseHandler.NotFound<IEnumerable<VolunteerModel>>(message: "There are no volunteers in this project.");

                var result = _mapper.Map<IEnumerable<VolunteerModel>>(projectVolunteers);
                return ResponseHandler.Success(data: result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteers in project data.");
                return ResponseHandler.BadRequest<IEnumerable<VolunteerModel>>(errors: ex.Message);
            }
        }
    }
}
