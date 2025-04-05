using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class ProjectVolunteerRepository : GenericRepository<ProjectVolunteer>, IProjectVolunteerRepository
    {
        public ProjectVolunteerRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
