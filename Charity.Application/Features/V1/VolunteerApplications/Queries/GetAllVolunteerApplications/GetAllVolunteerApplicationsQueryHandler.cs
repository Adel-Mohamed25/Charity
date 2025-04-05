using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetAllVolunteerApplications
{
    public class GetAllVolunteerApplicationsQueryHandler :
        IRequestHandler<GetAllVolunteerApplicationsQuery,
            Response<IEnumerable<VolunteerApplicationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllVolunteerApplicationsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllVolunteerApplicationsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllVolunteerApplicationsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<VolunteerApplicationModel>>> Handle(GetAllVolunteerApplicationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplications = await _unitOfWork.VolunteerApplications.GetAllAsync(orderBy: va => va.Id,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!volunteerApplications.Any())
                    return ResponseHandler.NotFound<IEnumerable<VolunteerApplicationModel>>(message: "Volunteer applications not found.");

                var result = _mapper.Map<IEnumerable<VolunteerApplicationModel>>(volunteerApplications);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer applications data.");
                return ResponseHandler.BadRequest<IEnumerable<VolunteerApplicationModel>>(errors: ex.Message);
            }
        }
    }
}
