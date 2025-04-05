using Charity.Contracts.RepositoriesAbstraction.IdentityRepositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.RepositoriesImplementation.IdentityRepository
{
    public class JwtTokenRepository : GenericRepository<JwtToken>, IJwtTokenRepository
    {
        public JwtTokenRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
