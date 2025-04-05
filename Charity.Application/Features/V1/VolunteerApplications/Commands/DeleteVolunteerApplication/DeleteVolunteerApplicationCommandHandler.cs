using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Commands.DeleteVolunteerApplication
{
    public class DeleteVolunteerApplicationCommandHandler :
        IRequestHandler<DeleteVolunteerApplicationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteVolunteerApplicationCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteVolunteerApplicationCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteVolunteerApplicationCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(DeleteVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplication = await _unitOfWork.VolunteerApplications.GetByAsync(p => p.Id.Equals(request.Id), cancellationToken: cancellationToken);
                if (volunteerApplication is null)
                    return ResponseHandler.NotFound<string>(message: "Volunteer application not found.");

                await _unitOfWork.VolunteerApplications.DeleteAsync(volunteerApplication, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The volunteer application has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete volunteer application.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
