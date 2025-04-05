using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class UserVolunteerActivityRepository : GenericRepository<UserVolunteerActivity>, IUserVolunteerActivityRepository
    {
        public UserVolunteerActivityRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
