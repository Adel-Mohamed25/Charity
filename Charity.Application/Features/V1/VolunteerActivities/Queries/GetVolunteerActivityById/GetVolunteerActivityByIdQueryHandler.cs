using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetVolunteerActivityById
{
    public class GetVolunteerActivityByIdQueryHandler :
        IRequestHandler<GetVolunteerActivityByIdQuery,
            Response<VolunteerActivityModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetVolunteerActivityByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetVolunteerActivityByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetVolunteerActivityByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<VolunteerActivityModel>> Handle(GetVolunteerActivityByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerActivity = await _unitOfWork.VolunteerActivities.GetByAsync(u => u.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (volunteerActivity is null)
                    return ResponseHandler.NotFound<VolunteerActivityModel>(message: "Volunteer activity not found.");

                var result = _mapper.Map<VolunteerActivityModel>(volunteerActivity);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer activity data.");
                return ResponseHandler.BadRequest<VolunteerActivityModel>(errors: ex.Message);
            }
        }
    }
}
