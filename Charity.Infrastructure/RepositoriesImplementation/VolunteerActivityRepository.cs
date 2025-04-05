using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class VolunteerActivityRepository : GenericRepository<VolunteerActivity>, IVolunteerActivityRepository
    {
        public VolunteerActivityRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
