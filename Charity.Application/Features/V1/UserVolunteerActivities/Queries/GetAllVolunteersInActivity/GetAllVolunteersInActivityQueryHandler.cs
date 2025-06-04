using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Queries.GetAllVolunteersInActivity
{
    public class GetAllVolunteersInActivityQueryHandler :
        IRequestHandler<GetAllVolunteersInActivityQuery,
            Response<IEnumerable<VolunteerModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllVolunteersInActivityQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllVolunteersInActivityQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllVolunteersInActivityQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<VolunteerModel>>> Handle(GetAllVolunteersInActivityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerActivity = await _unitOfWork.VolunteerActivities.GetByAsync(p => p.Id.Equals(request.VolunteerActivityId));
                if (volunteerActivity == null)
                    return ResponseHandler.NotFound<IEnumerable<VolunteerModel>>(message: "Volunteer Activity not found.");

                var userVolunteerActivities = await _unitOfWork.UserVolunteerActivities
                    .GetAllAsync(pv => pv.VolunteerActivityId.Equals(request.VolunteerActivityId),
                    includes: "User");

                if (!userVolunteerActivities.Any())
                    return ResponseHandler.NotFound<IEnumerable<VolunteerModel>>(message: "There are no volunteers in this volunteer activity.");

                var result = await userVolunteerActivities.ProjectTo<VolunteerModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponseHandler.Success(data: result.AsEnumerable());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteers in volunteer activity data.");
                return ResponseHandler.BadRequest<IEnumerable<VolunteerModel>>(errors: ex.Message);
            }
        }
    }
}
