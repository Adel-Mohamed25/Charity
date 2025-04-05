using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class MonetaryDonationRepository : GenericRepository<MonetaryDonation>, IMonetaryDonationRepository
    {
        public MonetaryDonationRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
