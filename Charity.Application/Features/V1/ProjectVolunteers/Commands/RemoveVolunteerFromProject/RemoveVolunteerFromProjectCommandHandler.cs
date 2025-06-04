using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.ProjectVolunteers.Commands.RemoveVolunteerFromProject
{
    public class RemoveVolunteerFromProjectCommandHandler :
        IRequestHandler<RemoveVolunteerFromProjectCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveVolunteerFromProjectCommand> _logger;
        private readonly IMapper _mapper;

        public RemoveVolunteerFromProjectCommandHandler(IUnitOfWork unitOfWork,
            ILogger<RemoveVolunteerFromProjectCommand> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(RemoveVolunteerFromProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projecVolunteer = await _unitOfWork.ProjectVolunteers
                    .GetByAsync(pv => pv.VolunteerId.Equals(request.ProjectVolunteer.VolunteerId)
                    && pv.ProjectId.Equals(request.ProjectVolunteer.ProjectId),
                    cancellationToken: cancellationToken);

                if (projecVolunteer is null)
                    return ResponseHandler.NotFound<string>(message: "The volunteer is not exist in this project.");

                var result = _mapper.Map<ProjectVolunteer>(request.ProjectVolunteer);
                await _unitOfWork.ProjectVolunteers.DeleteAsync(result, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.NoContent<string>(message: "The volunteer has been successfully removed from the project.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during remove volunteer from project.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
