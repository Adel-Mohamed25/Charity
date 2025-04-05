using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Commands.AddVolunteerToActivity
{
    public class AddVolunteerToActivityCommandHandler
        : IRequestHandler<AddVolunteerToActivityCommand,
            Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddVolunteerToActivityCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddVolunteerToActivityCommandHandler(IUnitOfWork unitOfWork,
            ILogger<AddVolunteerToActivityCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddVolunteerToActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userVolunteerActivity = await _unitOfWork.UserVolunteerActivities
                    .GetByAsync(pv => pv.UserId.Equals(request.UserVolunteerActivity.UserId)
                    && pv.VolunteerActivityId.Equals(request.UserVolunteerActivity.VolunteerActivityId),
                    cancellationToken: cancellationToken);

                if (userVolunteerActivity is not null)
                    return ResponseHandler.Conflict<string>(message: "The volunteer is already in this volunteer activity.");

                var result = _mapper.Map<UserVolunteerActivity>(request.UserVolunteerActivity);
                await _unitOfWork.UserVolunteerActivities.CreateAsync(result, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The volunteer has been successfully added to the volunteer activity.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during add volunteer to volunteer activity.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
