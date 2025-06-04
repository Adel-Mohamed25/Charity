using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.ProjectVolunteers.Commands.AddVolunteerToProject
{
    public class AddVolunteerToProjectCommandHandler : IRequestHandler<AddVolunteerToProjectCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<AddVolunteerToProjectCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddVolunteerToProjectCommandHandler(IUnitOfWork unitOfWork,
            IUnitOfService unitOfService,
            ILogger<AddVolunteerToProjectCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _unitOfService = unitOfService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddVolunteerToProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projecVolunteer = await _unitOfWork.ProjectVolunteers
                    .GetByAsync(pv => pv.VolunteerId.Equals(request.ProjectVolunteer.VolunteerId)
                    && pv.ProjectId.Equals(request.ProjectVolunteer.ProjectId),
                    cancellationToken: cancellationToken);

                if (projecVolunteer is not null)
                    return ResponseHandler.Conflict<string>(message: "The volunteer is already in this project.");

                var result = _mapper.Map<ProjectVolunteer>(request.ProjectVolunteer);
                await _unitOfWork.ProjectVolunteers.CreateAsync(result, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Created<string>(message: "The volunteer has been successfully added to the project.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during add volunteer to project.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
