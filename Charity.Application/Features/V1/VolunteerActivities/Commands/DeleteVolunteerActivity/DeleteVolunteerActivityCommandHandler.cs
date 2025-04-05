using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerActivities.Commands.DeleteVolunteerActivity
{
    public class DeleteVolunteerActivityCommandHandler : IRequestHandler<DeleteVolunteerActivityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteVolunteerActivityCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteVolunteerActivityCommandHandler(IUnitOfWork unitOfWork,
            ILogger<DeleteVolunteerActivityCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteVolunteerActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerActivity = await _unitOfWork.VolunteerActivities.GetByAsync(p => p.Id.Equals(request.Id), cancellationToken: cancellationToken);
                if (volunteerActivity is null)
                    return ResponseHandler.NotFound<string>(message: "Volunteer activity not found.");

                await _unitOfWork.VolunteerActivities.DeleteAsync(volunteerActivity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The volunteer activity has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete volunteer activity.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
