using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class InKindDonationRepository : GenericRepository<InKindDonation>, IInKindDonationRepository
    {
        public InKindDonationRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
