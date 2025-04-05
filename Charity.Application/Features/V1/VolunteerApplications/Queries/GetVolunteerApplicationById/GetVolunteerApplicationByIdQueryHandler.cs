using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetVolunteerApplicationById
{
    public class GetVolunteerApplicationByIdQueryHandler :
        IRequestHandler<GetVolunteerApplicationByIdQuery,
            Response<VolunteerApplicationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetVolunteerApplicationByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetVolunteerApplicationByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetVolunteerApplicationByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<VolunteerApplicationModel>> Handle(GetVolunteerApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplication = await _unitOfWork.VolunteerApplications.GetByAsync(u => u.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (volunteerApplication is null)
                    return ResponseHandler.NotFound<VolunteerApplicationModel>(message: "Volunteer application not found.");

                var result = _mapper.Map<VolunteerApplicationModel>(volunteerApplication);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer application data.");
                return ResponseHandler.BadRequest<VolunteerApplicationModel>(errors: ex.Message);
            }
        }
    }
}
