using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Commands.RemoveVolunteerFromActivity
{
    public class RemoveVolunteerFromActivityCommandHandler :
        IRequestHandler<RemoveVolunteerFromActivityCommand,
            Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveVolunteerFromActivityCommandHandler> _logger;
        private readonly IMapper _mapper;

        public RemoveVolunteerFromActivityCommandHandler(IUnitOfWork unitOfWork,
            ILogger<RemoveVolunteerFromActivityCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(RemoveVolunteerFromActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userVolunteerActivity = await _unitOfWork.UserVolunteerActivities
                    .GetByAsync(pv => pv.UserId.Equals(request.UserVolunteerActivity.UserId)
                    && pv.VolunteerActivityId.Equals(request.UserVolunteerActivity.VolunteerActivityId),
                    cancellationToken: cancellationToken);

                if (userVolunteerActivity is null)
                    return ResponseHandler.NotFound<string>(message: "The volunteer is not exist in this activity.");

                var result = _mapper.Map<UserVolunteerActivity>(request.UserVolunteerActivity);
                await _unitOfWork.UserVolunteerActivities.DeleteAsync(result, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.NoContent<string>(message: "The volunteer has been successfully removed from the activity.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during remove volunteer from activity.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
