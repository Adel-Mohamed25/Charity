using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class AidDistributionRepository : GenericRepository<AidDistribution>, IAidDistributionRepository
    {
        public AidDistributionRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
