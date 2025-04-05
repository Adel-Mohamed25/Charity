using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Models.ProjectVolunteer;

namespace Charity.Services.ServicesImplementation
{
    public class ProjectVolunteerServices : IProjectVolunteerServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectVolunteerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddVolunteerToProject(ProjectVolunteerModel projectVolunteerModel, CancellationToken cancellationToken)
        {
            var exists = await _unitOfWork.ProjectVolunteers
                .IsExistAsync(pv => pv.VolunteerId == projectVolunteerModel.VolunteerId
                && pv.ProjectId == projectVolunteerModel.ProjectId);

            if (exists)
                return false;

            var projectVolunteer = new ProjectVolunteer
            {
                VolunteerId = projectVolunteerModel.VolunteerId,
                ProjectId = projectVolunteerModel.ProjectId,
                JoinDate = DateTime.Now
            };

            await _unitOfWork.ProjectVolunteers.CreateAsync(projectVolunteer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> RemoveVolunteerFromProject(ProjectVolunteerModel projectVolunteerModel, CancellationToken cancellationToken)
        {
            var projectVolunteer = await _unitOfWork.ProjectVolunteers
                .GetByAsync(pv => pv.VolunteerId == projectVolunteerModel.VolunteerId
                && pv.ProjectId == projectVolunteerModel.ProjectId);

            if (projectVolunteer == null)
                return false;

            await _unitOfWork.ProjectVolunteers.DeleteAsync(projectVolunteer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
