using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetAllVolunteerActivities
{
    public class GetAllVolunteerActivitiesQueryHandler :
        IRequestHandler<GetAllVolunteerActivitiesQuery,
            Response<IEnumerable<VolunteerActivityModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllVolunteerActivitiesQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllVolunteerActivitiesQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllVolunteerActivitiesQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<VolunteerActivityModel>>> Handle(GetAllVolunteerActivitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerActivities = await _unitOfWork.VolunteerActivities.GetAllAsync(orderBy: va => va.Name,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!volunteerActivities.Any())
                    return ResponseHandler.NotFound<IEnumerable<VolunteerActivityModel>>(message: "Volunteer activities not found.");

                var result = await volunteerActivities.ProjectTo<VolunteerActivityModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponseHandler.Success(data: result.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer activities data.");
                return ResponseHandler.BadRequest<IEnumerable<VolunteerActivityModel>>(errors: ex.Message);
            }
        }
    }
}
