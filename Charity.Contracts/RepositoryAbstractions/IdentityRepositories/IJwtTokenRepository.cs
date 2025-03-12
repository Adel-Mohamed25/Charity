using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Contracts.RepositoryAbstractions.IdentityRepositories
{
    public interface IJwtTokenRepository : IGenericRepository<JwtToken>
    {
    }
}
