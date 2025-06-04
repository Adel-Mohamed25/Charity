using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Commands.UpdateVolunteerApplication
{
    public class UpdateVolunteerApplicationCommandHandler :
        IRequestHandler<UpdateVolunteerApplicationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateVolunteerApplicationCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateVolunteerApplicationCommandHandler(IUnitOfWork unitOfWork,
            ILogger<UpdateVolunteerApplicationCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplication = await _unitOfWork.VolunteerApplications.GetByAsync(p => p.Id.Equals(request.VolunteerApplicationModel.Id),
                    cancellationToken: cancellationToken);

                if (volunteerApplication is null)
                    return ResponseHandler.NotFound<string>(message: "Volunteer application not found.");

                _mapper.Map(request.VolunteerApplicationModel, volunteerApplication);
                await _unitOfWork.VolunteerApplications.UpdateAsync(volunteerApplication, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.NoContent<string>(message: "The volunteer application has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update volunteer application.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
