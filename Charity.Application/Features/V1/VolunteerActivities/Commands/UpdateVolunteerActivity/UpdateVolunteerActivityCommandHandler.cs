using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerActivities.Commands.UpdateVolunteerActivity
{
    public class UpdateVolunteerActivityCommandHandler : IRequestHandler<UpdateVolunteerActivityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateVolunteerActivityCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateVolunteerActivityCommandHandler(IUnitOfWork unitOfWork,
            ILogger<UpdateVolunteerActivityCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateVolunteerActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerActivity = await _unitOfWork.VolunteerActivities.GetByAsync(p => p.Id.Equals(request.VolunteerActivityModel.Id),
                    cancellationToken: cancellationToken);

                if (volunteerActivity is null)
                    return ResponseHandler.NotFound<string>(message: "Volunteer activity not found.");

                _mapper.Map(request.VolunteerActivityModel, volunteerActivity);
                await _unitOfWork.VolunteerActivities.UpdateAsync(volunteerActivity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.NoContent<string>(message: "The volunteer activity has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update volunteer activity.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}

