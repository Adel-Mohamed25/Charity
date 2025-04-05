using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerApplications.Commands.CreateVolunteerApplication
{
    public class CreateVolunteerApplicationCommandHandler :
        IRequestHandler<CreateVolunteerApplicationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateVolunteerApplicationCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateVolunteerApplicationCommandHandler(IUnitOfWork unitOfWork,
            ILogger<CreateVolunteerApplicationCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerApplication = _mapper.Map<VolunteerApplication>(request.VolunteerApplicationModel);
                await _unitOfWork.VolunteerApplications.CreateAsync(volunteerApplication, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The volunteer application has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create volunteer application.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
