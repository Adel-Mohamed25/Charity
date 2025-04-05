using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class VolunteerApplicationRepository : GenericRepository<VolunteerApplication>, IVolunteerApplicationRepository
    {
        public VolunteerApplicationRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
