using Charity.Contracts.RepositoriesAbstraction;
using Charity.Domain.Entities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation
{
    public class AssistanceRequestRepository : GenericRepository<AssistanceRequest>, IAssistanceRequestRepository
    {
        public AssistanceRequestRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
