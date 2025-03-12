using Charity.Contracts.RepositoryAbstractions.IdentityRepositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Persistence.Context;

namespace Charity.Infrastructure.Implementations.IdentityRepository
{
    public class JwtTokenRepository : GenericRepository<JwtToken>, IJwtTokenRepository
    {
        public JwtTokenRepository(ICharityDbContext context) : base(context)
        {
        }
    }
}
