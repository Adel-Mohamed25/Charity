using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class CharityProjectRepository : GenericRepository<CharityProject>, ICharityProjectRepository
    {
        public CharityProjectRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
