using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerActivities.Commands.CreateVolunteerActivity
{
    public class CreateVolunteerActivityCommandHandler : IRequestHandler<CreateVolunteerActivityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateVolunteerActivityCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateVolunteerActivityCommandHandler(IUnitOfWork unitOfWork,
            ILogger<CreateVolunteerActivityCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateVolunteerActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerActivity = _mapper.Map<VolunteerActivity>(request.VolunteerActivityModel);
                await _unitOfWork.VolunteerActivities.CreateAsync(volunteerActivity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Created<string>(message: "The volunteer activity has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create volunteer activity.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
